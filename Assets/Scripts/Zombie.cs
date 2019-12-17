using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    //Delay for the next movement patrol
    [SerializeField] private float delayNextMoveTime;
    
    [SerializeField] private int health;
    [SerializeField] private float speed;
    private bool dead;
    private bool hit;

    private GameObject parent;
    private Animator animator;
    private PolygonCollider2D collider;
    private SpriteRenderer renderer;

    [SerializeField] private Transform[] patrolPoints;
    private int indexPatrol;

    //For toggle delayMove
    private bool toggleMoveDelay; 

    public void Initialize(int health, float speed)
    {
        parent = transform.parent.gameObject;
        this.health = health;
        this.speed = speed;
        animator = GetComponent<Animator>();
        collider = GetComponent<PolygonCollider2D>();
        renderer = GetComponent<SpriteRenderer>(); 
        toggleMoveDelay = true;
        FlipImage();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize(3,2);
    }

    // Update is called once per frame
    void Update()
    {
        //To freeze the animation attach to this object and resume it when unpause
        animator.enabled = !GameManagement._instance.PauseGame;
        
        if (!GameManagement._instance.StartGame || GameManagement._instance.PauseGame) return;
        
        if (dead || hit) return;
        if (toggleMoveDelay) Walk();
    }

    public void Hit()
    {
        if (dead) return;

        StopCoroutine(ToggleHit());
        
        animator.SetTrigger("Hit");
        
        health--;
        if (health <= 0)
        {
            Dead();
        }
        
        StartCoroutine(ToggleHit());
    }

    private void Dead()
    {
        health = 0;
        dead = true;
        animator.SetBool("Death", true);
        GameManagement._instance.DecreaseZombieCount();
        Destroy(collider);
    }

    private void Walk()
    {
        if (patrolPoints.Length == 0) return;

        animator.SetBool("Walk", true);
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[indexPatrol].position, speed * Time.deltaTime);
        
        if (transform.position == patrolPoints[indexPatrol].position)
        {
            animator.SetBool("Walk", false);
            toggleMoveDelay = false;
            indexPatrol++;
            if (indexPatrol == patrolPoints.Length) indexPatrol = 0;
            StartCoroutine(DelayNextMove());
        }
    }

    IEnumerator DelayNextMove()
    {
        yield return new WaitUntil(UnHitted);
        yield return new WaitForSeconds(delayNextMoveTime);
        yield return new WaitUntil(UnHitted);
        FlipImage();
        toggleMoveDelay = true;
    }

    private void FlipImage()
    {
        float difference = patrolPoints[indexPatrol].position.x - transform.position.x;
        renderer.flipX = difference < 0;
    }

    private void OnMouseDown()
    {
        if (!GameManagement._instance.HitAble) return;
            
        hit = true;
        Hit();
    }

    
    IEnumerator ToggleHit()
    {
        yield return new WaitUntil(UnHitted);
        hit = false;
    }

    //To get is zombie is not unhitted again when he got hit then return the opposite and also
    // when he didnt got hit will return the opposite too
    public bool UnHitted()
    {
        return !animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieHit");
    }
}
