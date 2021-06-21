using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{

    Animator animator;
    Zombie zombie;
    NavMeshAgent nm;
    Transform target;

    public float detectionThreshhold = 25f;
    public float attackThreshhold = 1.0f;

    public bool isMoving;
    public bool isAttacking;
    public bool isStaggered;
    public bool isDead;

    float attackStart;
    float attackOffset = 0.6f;
    bool hasDamaged;
    float waitTime;
    float startTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        zombie = GetComponent<Zombie>();
        nm = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        hasDamaged = true;
        waitTime = Random.Range(0f, 0.25f);
        startTime = Time.time;
    }

    void LateUpdate()
    {
        if (Time.time > startTime + waitTime) {
            SetCurrentAnimation();
            if (target.tag == "Player") {
                if (!isDead) {
                    float distance = Vector3.Distance(target.position, transform.position);
                    if (distance < attackThreshhold) {
                        Attack();
                    }
                    else if (distance < detectionThreshhold) {
                        Move();
                    }
                    else {
                        animator.SetBool("Chasing", false);
                    }
                    handleAttack();
                    handleMove();
                    handleStagger();
                }
                else {
                    handleDeath();
                }

            }  
            else{
                nm.SetDestination(transform.position);
                animator.SetBool("Chasing",false);
                animator.SetBool("Attacking",false);
                handleStagger();
            }
        }
    }

    void Move(){
        if(!isStaggered && !isAttacking){
            animator.SetBool("Chasing",true);
        }
    }

    void Attack(){
        if(!isAttacking && !isStaggered){
            animator.SetBool("Attacking",true);
            attackStart = Time.time;
            hasDamaged = false;
        }
    }

    public void StartStagger(){
        animator.SetBool("isHit",true);
    }

    public void StartDie(){
        GetComponent<CapsuleCollider>().enabled = false;
        nm.SetDestination(transform.position);
        nm.enabled = false;
        animator.SetBool("isDead",true);
    }

    void handleAttack(){
        if(isAttacking){
            animator.SetBool("Attacking",false);
            if(!hasDamaged && Time.time > attackStart + attackOffset){
                Collider[] hitColliders = Physics.OverlapBox(new Vector3(transform.position.x,1,transform.position.z) + transform.rotation * Quaternion.Euler(0f,90,0f) * new Vector3(-1f,0,0), new Vector3(1.5f,0f,1.4f) / 2, transform.rotation * Quaternion.Euler(0f,90,0f));
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].tag == "Player")
                    {
                        hitColliders[i].GetComponent<Player>().TakeDamage(zombie.Damage);
                    }
                }
                hasDamaged = true;
            }
        }
    }

    void handleDeath(){
        if(isDead){
            animator.SetBool("isDead",false); // consider renaming Bool
        }
    }

    void handleMove(){
        if(isMoving && nm.enabled){
            nm.SetDestination(target.position);
        }
    }

    void handleStagger(){
        if(isStaggered){
            animator.SetBool("isHit",false); // TRY THIS OUT
        }
    }

    void SetCurrentAnimation(){
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_walking")){
            SetAnimatorBools(true,false,false,false);
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_hitting")){
            SetAnimatorBools(false,true,false,false);
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_stagger")){
            SetAnimatorBools(false,false,true,false);
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_dying")){
            SetAnimatorBools(false,false,false,true);
        }
        else{
            SetAnimatorBools(false,false,false,false);
        }
    }

    void SetAnimatorBools(bool move, bool attack, bool stagger, bool dead){
        isMoving = move;
        isAttacking = attack;
        isStaggered = stagger;
        isDead = dead;
    }

}
