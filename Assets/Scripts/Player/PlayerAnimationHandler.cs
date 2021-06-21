using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    PlayerMovement playerMovement;
    Animator animator;
    public float velocity = 0.0f;
    public bool isRolling;
    public bool isRollAnimation;
    bool animatorRoll;
    bool animatorHit;
    bool animatorHit2;
    bool animatorStagger;
    bool staggerHelper;
    bool staggerHelper2;

    float attackTime;
    bool hasDamaged;
    [SerializeField] float attackDelay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update() {
        animator.SetFloat("Velocity", velocity);
        

        CheckAnimationTransition();
        SetPrevAnimation();
        DoAttack();

        // Trigger Stagger correctly
        if(staggerHelper2){
            staggerHelper2 = false;
            animator.SetBool("isHit",false);
        }
        if(staggerHelper){
            staggerHelper = false;
            staggerHelper2 = true;
        }
    }

    private void SetPrevAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("roll"))
        {
            SetAnimatorBools(true, false, false, false);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit"))
        {
            SetAnimatorBools(false, true, false, false);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            SetAnimatorBools(false, false, true, false);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("playerStagger"))
        {
            SetAnimatorBools(false, false, false, true);
        }
        else SetAnimatorBools(false, false, false, false);

    }

    private void CheckAnimationTransition() {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("death")){
            animator.SetBool("isDead",false);
            playerMovement.canBeHit = false;
            playerMovement.canAct = false;
        }
        if (animatorRoll && !animator.GetCurrentAnimatorStateInfo(0).IsName("roll"))
        {
            playerMovement.isRolling = false;
            playerMovement.canAct = true;
            playerMovement.canBeHit = true;
            animator.SetBool("isRolling", false);
        }
        else if(animatorHit && !animator.GetCurrentAnimatorStateInfo(0).IsName("hit")){
            playerMovement.isAttacking = false;
            playerMovement.canAct = true;
            animator.SetBool("isLightHitting",false);
        }
        else if(animatorStagger && !animator.GetCurrentAnimatorStateInfo(0).IsName("playerStagger")){
            playerMovement.canAct = true;
            animator.SetBool("isLightHitting",false);
        }
       
    }

    public void StartRoll() {
        animator.SetBool("isRolling", true);
    }

    public void StartAttack(){
        animator.SetBool("isLightHitting", true);
        attackTime = Time.time;
        hasDamaged = false;
    }

    public void StartStagger(){
        animator.SetBool("isHit",true);
        staggerHelper = true;
        staggerHelper2 = false;
    }

    public void StartDeath(){
        animator.SetBool("isDead",true);
        playerMovement.canBeHit = false;
    }

    private void SetAnimatorBools(bool roll, bool hit, bool hit2, bool stagger) { 
        animatorRoll = roll;
        animatorHit = hit;
        animatorHit2 = hit2;
        animatorStagger = stagger;
    }

    private void DoAttack(){
        if((animatorHit || animatorHit2) && !hasDamaged && Time.time > attackTime+attackDelay){
            playerMovement.checkAttackTargets();
            hasDamaged = true;
        }
    }

}
