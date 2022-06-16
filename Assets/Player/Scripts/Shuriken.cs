using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       // animator.SetTrigger("Shuriken");
    }

    void OnCollisionEnter2D()
    {
        Destroy(this.gameObject);
    }
}
