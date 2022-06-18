using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    private int count;
    public int health;

    void Start()
    {
      count = 2;
      health = 100;
    }

    void Update() 
    {
        {
            if (count == 0)
            {
                Destroy(gameObject);
            }
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
