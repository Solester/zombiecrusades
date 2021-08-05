using UnityEngine;
using System.Collections;
using System.Linq;

public class Zombie : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public Transform groundCheck;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool touchedWall;
    private bool facingRight;
    private int layerMask;
    
    public int health;
    public float speed;
    public bool isDead;

    void Start()
    {
        touchedWall = false;
        facingRight = true;
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        layerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        if (isDead)
            return;

        if (touchedWall)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            speed *= -1;
        }
            touchedWall = Physics2D.Linecast(transform.position, groundCheck.position, layerMask);
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;

        rb2D.velocity = new Vector2(speed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Attack"))
        {
            DamageEnemy();
        }
    }

    private void DamageEnemy()
    {
        health--;

        StartCoroutine(DamageEffect());

        if (health == 0)
        {
            isDead = true;
            rb2D.velocity = new Vector2(0f, 0f);
            anim.SetBool("isDead", true);
            StartCoroutine(DeadEnemy());
        }
    }

    IEnumerator DamageEffect()
    {
        float auxSpeed = speed;
        speed = speed * -1;
        sprite.color = Color.red;
        rb2D.AddForce(new Vector2(0f, 200f));
        yield return new WaitForSeconds(.1f);
        speed = speed * -1;
        speed = auxSpeed;
        sprite.color = Color.white;
    }
    
    IEnumerator DeadEnemy()
    {
        yield return new WaitForSeconds(.8f);
        Destroy(gameObject);
    }
       
}
