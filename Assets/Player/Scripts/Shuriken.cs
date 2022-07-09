using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private int damage = 30;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<enemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        // if(other.gameObject.tag == "Platform")
        // {
        //     Destroy(this.gameObject);
        // }
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }    
}
