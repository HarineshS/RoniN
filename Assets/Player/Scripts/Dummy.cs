using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public int health;

    void Start()
    {
      health = 100;
    }

    void Update() 
    {
        {
            if (health <= 0)
            {
                Debug.Log("Died!!");
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}
