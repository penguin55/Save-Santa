using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private int health;
    private float speed;
    private bool dead;

    private Animator animator;

    public void Initialize(int health, float speed)
    {
        this.health = health;
        this.speed = speed;
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;
    }

    public void Hit()
    {
        if (dead) return;

        animator.SetTrigger("Hit");
        health--;
        if (health == 0)
        {
            animator.SetBool("Death", true);
        }
    }

    private void Walk()
    {

    }
}
