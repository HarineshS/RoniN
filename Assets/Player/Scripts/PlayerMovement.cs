using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float direction;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveRight()
    {
       
    }
    public void MoveLeft()
    {
         gameObject.transform.Translate(new Vector3(-1,0,0) * speed * Time.deltaTime);
    }

    Void FixedUpdate()
    {
        
    }
}
