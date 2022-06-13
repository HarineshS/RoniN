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
    
    public float speed = 5f;
    public float jumpForce=7f;

     void Awake()
    {
        isGround = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < Mathf.Epsilon) 
        {
            isGround = false;
            animator.SetBool("isJumping",true);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);   
        }
    }

    

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Platform")
        {
            isGround = true;
            animator.SetBool("isJumping",false);
        }
    }
}
