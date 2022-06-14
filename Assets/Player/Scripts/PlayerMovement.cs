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
    public static bool isPaused;
    
    public float speed = 5f;
    public float jumpForce=7f;
    public Joystick joystick;
    public int damage;


     void Awake()
    {
        isGround = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isPaused= false;
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
    }
    
    void FixedUpdate()
    {
        KeyboardMovement();  
        //JoystickMovement(); 
    }
    void KeyboardMovement()
    {
        transform.position += new Vector3(horizontalMovement, 0,0) * speed * Time.deltaTime;
        if(isRight && horizontalMovement > 0f)
        {
            transform.localScale = new Vector3(.25f,.25f,1);
            isRight = false;
        }
        else if(!isRight && horizontalMovement < 0f)
        {
            transform.localScale = new Vector3(-.25f,.25f,1);
            isRight = true;
        }

        //Jump
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < Mathf.Epsilon) 
        {
            isGround = false;
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

        //pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused=true;
            if (isPaused)
            {
                isPaused = false;
            }
            else if (!isPaused)
            {
                isPaused = true;
            }
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
            isGround = true;
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
        }
        if(!isKatana)
        {
            animator.SetBool("shuriken",true);
            damage = 50;
        }
    }
}
