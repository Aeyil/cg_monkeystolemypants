using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{

    NavMeshAgent nm;
    public Transform target;

    public float distanceThreshhold = 10f;
    public float attackThreshhold = 1.0f;

    public bool staggerHelper;
    public bool staggerHelper2;

    public bool deadHelper;
    public bool deadHelper2;

    Zombie zombie;

    public enum AIState { idle, chasing, attack, staggered, dead};

    public AIState aiState = AIState.idle;

    public Animator animator;

    bool hasDamaged;
    float hitOffset = 0.5f;
    float hitStart;

    float LastStateHandleCall;
    float StateHandleOffset = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        zombie = GetComponent<Zombie>();
        nm = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        hasDamaged = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(LastStateHandleCall +  StateHandleOffset < Time.time){
            switch (aiState)
            {
                case AIState.idle:
                    float dist = Vector3.Distance(target.position, transform.position);
                    if (dist < distanceThreshhold)
                    {
                        aiState = AIState.chasing;
                        animator.SetBool("Chasing", true);
                    }
                    nm.SetDestination(transform.position);
                    break;
                case AIState.chasing:
                    dist = Vector3.Distance(target.position, transform.position);
                    if (dist > distanceThreshhold)
                    {
                        aiState = AIState.idle;
                        animator.SetBool("Chasing", false);
                    }
                    if (dist < attackThreshhold)
                    {
                        StartHit();
                    }
                    nm.SetDestination(target.position);
                    break;
                case AIState.attack:
                    animator.SetBool("Attacking", false);
                    nm.SetDestination(transform.position);
                    dist = Vector3.Distance(target.position, transform.position);

                    if (dist > attackThreshhold)
                    {
                        aiState = AIState.chasing;
                        animator.SetBool("Attacking", false);
                    }
                    else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_hitting"))
                    {
                        StartHit();
                    }           
                    break;
                case AIState.staggered:

                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_stagger"))
                    {
                        aiState = AIState.idle;
                        animator.SetBool("isHit", false);
                    }
                    break;
                case AIState.dead:
                    nm.SetDestination(transform.position);
                    break;
                default:
                    break;
            }
            LastStateHandleCall = Time.time;
        }

        if (!hasDamaged && Time.time > hitStart + hitOffset)
        {
            CheckForHit();
            hasDamaged = true;
        }
        if (aiState == AIState.staggered)
        {
            if (staggerHelper2)
            {
                staggerHelper2 = false;
                animator.SetBool("isHit", false);
            }
            if (staggerHelper)
            {
                staggerHelper = false;
                staggerHelper2 = true;
            }
        }

        if (aiState == AIState.dead)
        {
            if (deadHelper2)
            {
                deadHelper2 = false;
                animator.SetBool("isDead", false);
            }
            if (deadHelper)
            {
                deadHelper = false;
                deadHelper2 = true;
            }
        }
    }

    public void StartHit() {
        animator.SetBool("Attacking", true);
        aiState = AIState.attack;
        hitStart = Time.time;
        hasDamaged = false;
    }

    public void CheckForHit() {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + new Vector3(-1, 0, 0), transform.localScale / 2, Quaternion.identity);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Player")
            {
                hitColliders[i].GetComponent<Player>().TakeDamage(zombie.Damage);
            }
        }
    }

    public void StartDead() {
        GetComponent<CapsuleCollider>().enabled = false;
        aiState = AIState.dead;
        animator.SetBool("Attacking", false);
        animator.SetBool("isHit", false);
        animator.SetBool("isDead", true);
        deadHelper = true;
        deadHelper2 = false;
    }

    public void StartStagger() {
        aiState = AIState.staggered;
        animator.SetBool("isHit", true);
        staggerHelper = true;
        staggerHelper2 = false;
    }
}
