using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed;
    private bool moveright= true;
    public float distance;
    public float distancetocast;
    private bool val;

    public Rigidbody2D rb;
    private bool PlayerInRange;
    public Transform castPoint;
    private float castDist;
    private Transform target;

    private float TargetDistance;
    public float AttackRange;
    public float MinDistanceToFollow;
    public Animator EnemyAnimator;
    public int health;

    
    
    
    public Transform grounddetection;
   // Start is called before the first frame update
    void Start()
    {
        health = 100;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    
      work();
      if (health <= 0)
            {
                Debug.Log("Died!!");
                Destroy(gameObject);
            }

        
        
    }


    void work()
    {
        Debug.Log("working");
         if (!CanSeePlayer(distancetocast))
       {
        patrol();
       }
       else
       {
        chaseplayer();
       }
    }



    
    void patrol()
    {
        
       Debug.Log("patrolling");
        transform.Translate(Vector2.right* speed * Time.deltaTime);
        RaycastHit2D groundinfo = Physics2D.Raycast(grounddetection.position, Vector2.down, distance);

        Debug.DrawRay(grounddetection.position, Vector2.down * distance, Color.red);

        move();
    }

    // void follow()
    // {
    //     Debug.Log("following");
    //     transform.position = Vector2.MoveTowards(transform.position,target.position,speed*Time.deltaTime);

    // }

    void attack()
    {
        EnemyAnimator.SetBool("IsInRange", true);
        EnemyAnimator.SetBool("isRunning", false);
        Debug.Log("attacking");
    }


    void move()
    {
        Debug.Log("moving");
        EnemyAnimator.SetBool("isRunning", true);
         EnemyAnimator.SetBool("IsInRange", false);
        transform.Translate(Vector2.right* speed * Time.deltaTime);
        RaycastHit2D groundinfo = Physics2D.Raycast(grounddetection.position, Vector2.down, distance);

        Debug.DrawRay(grounddetection.position, Vector2.down * distance, Color.red);

            if ( groundinfo .collider ==false)
            {

                if (moveright == true)
                {

                    transform.eulerAngles = new Vector3(0, -180, 0);
                    moveright = false;

                }else
                {

                    transform.eulerAngles = new Vector3(0, 0, 0);
                    moveright = true;
                }

            }
    }
    void stopmovement()
    {
        EnemyAnimator.SetBool("IsInRange", false);
        EnemyAnimator.SetBool("isRunning", false);
        speed = 0;
        Debug.Log("stopped movement");

    }

    void chaseplayer()
    {
        Debug.Log("chasing");
        if(Vector2.Distance(transform.position, target.position)  > AttackRange && Vector2.Distance(transform.position, target.position) < MinDistanceToFollow)
        {
            //follow();
           //transform.position = Vector2.MoveTowards(transform.position,target.position,speed*Time.deltaTime);
            //transform.position = transform.Translate(Vector2(target.position.x,0),speed*Time.deltaTime);
            transform.position = Vector2.MoveTowards (transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);

            //move();
        
            

            
        }
        else if(Vector2.Distance(transform.position, target.position)  < AttackRange)
        {
        
        stopmovement();
        attack();
            
        }
        else
        {
            speed=2;
            patrol();

        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
    }

    









    bool CanSeePlayer(float distancetocast)
    {
        
        castDist = distancetocast;
        
        
        if(!moveright)
        {
            castDist = -distancetocast;
        }
        Vector2 endPos = castPoint.position + Vector3.right * castDist;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos,1<< LayerMask.NameToLayer("Enemy"));
        
        if(hit.collider !=null)
        {
            Debug.Log("something hit");
             Debug.Log(hit.collider.name);

             if(hit.collider.gameObject.CompareTag("Platform"))
             {
                 Debug.Log("goes in");  
                if ( hit.collider ==true)
                {

                    if (moveright == false)
                    {

                        transform.eulerAngles = new Vector3(0, 0, 0);
                        moveright = true;

                    }else
                    {

                        transform.eulerAngles = new Vector3(0, -180, 0);
                        moveright = false;
                    }

                }
             }
            
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                
                val = true;
                Debug.DrawLine(castPoint.position, hit.point,Color.red);
                Debug.Log("ray hit player");
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(castPoint.position, endPos,Color.green);
        }
        else
        {
            Debug.DrawLine(castPoint.position,endPos,Color.blue);
        }
        
        return val;
        
    }
}