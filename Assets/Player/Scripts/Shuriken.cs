using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private Animator animator;
    private int damage = 50;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       // animator.SetTrigger("Shuriken");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Dummy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "Platform")
        {
            Destroy(this.gameObject);
        }
    }    
}
