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
    private bool isGround;
    private bool isKatana;

    //shoot
    public Transform shootPoint;
    public float Force;
    public GameObject shuriken;
    public Vector2 Direction;
    public Transform Target;
    private bool Detected;
    private float castDist;
    private float fix;

    public float speed = 5f;
    public float jumpForce=7f;
    public Joystick joystick;

    //melee
    private float timeBWAttack;
    public float startTimeBWAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyMask;
    public int damage;

     void Awake()
    {
        isGround = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fix = 2;
        Vector2 targetPos = new Vector2(transform.position.x + fix , shootPoint.transform.position.y);
        Direction = targetPos - (Vector2)transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));

        Vector2 targetPos = new Vector2(transform.position.x + fix , shootPoint.transform.position.y);
        Direction = targetPos - (Vector2)transform.position;
    }
    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);   
    }
    
    void FixedUpdate()
    {
        KeyboardMovement();  
        //JoystickMovement(); 
        //SomeRaycastThing();
    }
    void KeyboardMovement()
    {
        transform.position += new Vector3(horizontalMovement, 0,0) * speed * Time.deltaTime;
        if(isRight && horizontalMovement > 0f)
        {
            transform.localScale = new Vector3(.25f,.25f,1);
            isRight = false;
            fix = 2;
        }
        else if(!isRight && horizontalMovement < 0f)
        {
            transform.localScale = new Vector3(-.25f,.25f,1);
            isRight = true;
            fix = -2;
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
            TimeBWAttack();
            isKatana = true;
            StartCoroutine(Attack(isKatana));
        }
        if (Input.GetButtonDown("Fire2"))
        {
            TimeBWAttack();
            isKatana = false;
            StartCoroutine(Attack(isKatana));
        }
    }

    void TimeBWAttack()
    {
        if(timeBWAttack <=0)
        {
            timeBWAttack = startTimeBWAttack;
        }
        else
        {
            timeBWAttack -= Time.deltaTime;
        }
    } 

    // void JoystickMovement()
    // {
    //     float joystickDirection = joystick.Horizontal;
    //     gameObject.transform.Translate(new Vector3(joystickDirection,0,0) * speed * Time.deltaTime);
    //     animator.SetFloat("Speed", Mathf.Abs(joystickDirection));
    //     if(isRight && joystickDirection > 0f)
    //     {
    //         transform.localScale = new Vector3(1,1,1);
    //         isRight = false;
    //     }
    //     else if(!isRight && joystickDirection < 0f)
    //     {
    //         transform.localScale = new Vector3(-1,1,1);
    //         isRight = true;
    //     }
    // }
    

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Platform")
        {
            animator.SetBool("isJumping",false);
        }
    }

    IEnumerator Attack(bool isKatana)
    {
        if(isKatana)
        {
            animator.SetBool("katana",true);
            yield return new WaitForSeconds(0.25f);
            animator.SetBool("katana",false);
            damage = 100;
            Melee(damage);
        }
        if(!isKatana)
        {
            animator.SetBool("shuriken",true);
            yield return new WaitForSeconds(0.25f);
            animator.SetBool("shuriken",false);
            shoot();
        }
    }
    
    void shoot()
    {
        GameObject ShurikenIns = Instantiate(shuriken, shootPoint.position, Quaternion.identity);
        ShurikenIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }

    void Melee(int damage)
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange,enemyMask);
        for (int i =0; i< enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Dummy>().TakeDamage(damage);
        }
    }
}
