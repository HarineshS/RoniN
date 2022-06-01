using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float horizontalMovement = 0f;
    private bool isRight;
    
    public float speed = 5f;
    public float jumpForce=7f;

     void Awake()
    {
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
        Movement();   
    }
    void Movement()
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
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);   
        }
    }
}
