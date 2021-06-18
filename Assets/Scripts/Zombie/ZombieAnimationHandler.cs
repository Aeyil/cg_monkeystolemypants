using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_movement : MonoBehaviour
{
    public Transform player;
    float MoveSpeed = 0.8f;
    int MaxDist = 10;
    float MinDist = 1.0f;
    Animator animator;
    float HittingRange = 1.5f;

    bool isHittingRange = false;
    bool isInSight = false;
    bool isHit = false;
    bool isDead = false;
    bool isHitting = false;
    bool isStaggered = false;
    bool isDying = false;

    //private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        //controller = this.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isHit = false;
        isDead = false;
        isHitting = animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_hitting");
        isStaggered = animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_stagger");
        isDying = animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_dying");


        animator.SetBool("isHit", false);
        if (Input.GetKey("m")) {
            isHit = true;
        }
        animator.SetBool("isDead", false);
        if (Input.GetKey("y"))
        {
            isDead = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_dying")) {
            isDead = false;
        }

        if (!isDead && !isStaggered && !isDying) {
            Move();
        } 
        

        animator.SetBool("isHittingRange", isHittingRange);
        animator.SetBool("isInSight", isInSight);
        animator.SetBool("isHit", isHit);
        animator.SetBool("isDead", isDead);
    }

    public void Move() {

        if (player != null)
        {
            transform.LookAt(player);
        }

        if (Vector3.Distance(transform.position, player.position) <= HittingRange)
        {
            isHittingRange = true;
        }
        else
        {
            isHittingRange = false;
        }

        if (Vector3.Distance(transform.position, player.position) >= MinDist && !isHitting && !isStaggered && !isDead)
        {
            isInSight = true;
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, player.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
    }
}
