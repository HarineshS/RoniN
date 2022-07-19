using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float horizontalMovement = 0f;
    private bool isRight;
    private bool isKatana;
    private bool check;

    //shoot
    public Transform shootPoint;
    public float Force;
    public GameObject shuriken;
    public Vector2 Direction;
    public Transform Target;
    private bool Detected;
    private float castDist;
    private float fix;
    public int currentCount;
    private int count = 5;
    public ShurikenCount shurikenCount;

    public float speed = 5f;
    public float jumpForce=7f;
    public UIManager ui;

    //melee
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyMask;
    public int damage;

    //health
    private int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    private int healthIncreased = 1;
    private int healthInterval = 5;

     void Awake()
    {
        check = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fix = 5;
        Vector2 targetPos = new Vector2(shootPoint.transform.position.x + fix , shootPoint.transform.position.y);
        Direction = targetPos - (Vector2)transform.position;
    }
    void Start()
    {
        currentCount = count;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
        shurikenCount.SetCount(currentCount);
        ui.OpenControls();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentHealth);
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
        Vector2 targetPos = new Vector2(shootPoint.transform.position.x + fix , shootPoint.transform.position.y);
        Direction = targetPos - (Vector2)transform.position;
    }
    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);   
    }
    
    void FixedUpdate()
    {
        KeyboardMovement(check);  
        shurikenCount.SetCount(currentCount);
    }
    void KeyboardMovement(bool check)
    {
       if (check)
       {
            transform.position += new Vector3(horizontalMovement, 0,0) * speed * Time.deltaTime;
            if(isRight && horizontalMovement > 0f)
            {
                transform.localScale= new Vector2(transform.localScale.x*-1,transform.localScale.y);
                isRight = false;
                fix = 5;
            }
            else if(!isRight && horizontalMovement < 0f)
            {
                transform.localScale= new Vector2(transform.localScale.x*-1,transform.localScale.y);

                isRight = true;
                fix = -5;
            }

            //Jump
            if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < Mathf.Epsilon) 
            {
                animator.SetBool("isJumping",true);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);   
            }

            //Attack
            if (Input.GetButtonDown("Fire1"))
            {
                isKatana = true;
                StartCoroutine(Attack(isKatana));
            }
            if (Input.GetButtonDown("Fire2"))
            {
                isKatana = false;
                StartCoroutine(Attack(isKatana));
            }

            if(Input.anyKey)
            {
            ui.CloseControls(); 
            }
        }
    }

        void OnCollisionEnter2D(Collision2D other) 
        {
            if(other.gameObject.tag == "Platform")
            {
                animator.SetBool("isJumping",false);
                StartCoroutine(Health());
            }
            if(other.gameObject.tag == "Enemy")
            {
                damage = 10;
                TakeDamage(damage);
            }
            if(other.gameObject.tag == "FallEdge")
            {
                Debug.Log("You Died!!");
                StartCoroutine(Death());
            }
            if(other.gameObject.tag == "Shuriken")
            {
                if(currentCount<9)
                {
                    currentCount += 1;
                }
            }
            if(other.gameObject.tag == "Finish")
            {
                    SceneManager.LoadScene(1);
            }
    }

    void OnCollisionStay2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            damage = 1;
            TakeDamage(damage);
        }  
    }

    IEnumerator Attack(bool isKatana)
    {
        if(isKatana)
        {
            animator.SetBool("katana",true);
            yield return new WaitForSeconds(0.25f);
            animator.SetBool("katana",false);
            damage = 50;
            Melee(damage);
        }
        if(!isKatana)
        {
            animator.SetBool("shuriken",true);
            yield return new WaitForSeconds(0.25f);
            animator.SetBool("shuriken",false);
            Shoot(currentCount);
        }
    }

    public void Shoot(int Count)
    {
        if(Count > 0)
        {
            GameObject ShurikenIns = Instantiate(shuriken, shootPoint.position, Quaternion.identity);
            ShurikenIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
            Count-- ;
            currentCount = Count;            
        }
        if(currentCount <=0)
        {
           Debug.LogWarning("NO Shurikens Left!!");
        }
    }

    void Melee(int damage)
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange,enemyMask);
        for (int i =0; i< enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<enemy>().TakeDamage(damage);
        }
    }

    IEnumerator Death()
    {
        check = false;
        animator.SetTrigger("death");
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
       // healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }

    void HealthRegeneration()
    {
        currentHealth += healthIncreased;
        //Debug.Log(currentHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    IEnumerator Health()
    {
        yield return new WaitForSeconds(2);
        if(currentHealth > 0)
        {
            for(int i = 0; i < healthInterval; i++) 
            {
                HealthRegeneration();
            }
        }
    }
}
