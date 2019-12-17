using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{

    //To determine is santa facing left or not
    [SerializeField] private bool faceLeft;
    private float speed;
    private Transform direction;

    private SpriteRenderer renderer;
    private Animator animator;

    public void Initialize(float speed, Transform direction)
    {
        this.speed = speed;
        this.direction = direction;
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        renderer.flipX = faceLeft;
        
    }

    // Update is called once per frame
    void Update()
    {
        //To freeze the animation attach to this object and resume it when unpause
        animator.enabled = !GameManagement._instance.PauseGame;
        
        if (!GameManagement._instance.StartGame  || GameManagement._instance.PauseGame) return;
        
        if (direction == null) return;
        animator.SetBool("Walk", true);
        transform.position = Vector2.MoveTowards(transform.position, direction.position, speed * Time.deltaTime);


        if (transform.position == direction.position)
        {
            Stop();
        }
    }
    
    public void WalkToDirection(Transform direction)
    {
        faceLeft = (direction.position.x - transform.position.x) < 0;
        this.direction = direction;
        renderer.flipX = faceLeft;
    }
    
    public void Stop()
    {
        direction = null;
        animator.SetBool("Walk", false);
    }

    public Transform GetDirection()
    {
        return direction;
    }
}
