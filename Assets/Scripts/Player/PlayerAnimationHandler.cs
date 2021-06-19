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
    int PrevAnimationHash;
    bool animatorRoll;
    bool animatorHit;
    bool animatorHit2;
    bool animatorStagger;
    bool staggerHelper;
    bool staggerHelper2;

    bool deadHelper;
    bool deadHelper2;

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

        // Trigger Stagger correctly
        if(staggerHelper2){
            staggerHelper2 = false;
            animator.SetBool("isHit",false);
        }
        if(staggerHelper){
            staggerHelper = false;
            staggerHelper2 = true;
        }
        // Trigger Death correctly
        if(deadHelper2){
            deadHelper2 = false;
            animator.SetBool("isDead",false);
        }
        if(deadHelper){
            deadHelper = false;
            deadHelper2 = true;
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
        if (animatorRoll && !animator.GetCurrentAnimatorStateInfo(0).IsName("roll"))
        {
            playerMovement.isRolling = false;
            playerMovement.canAct = true;
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
    }

    public void StartStagger(){
        animator.SetBool("isHit",true);
        staggerHelper = true;
        
    }

    public void StartDeath(){
        animator.SetBool("isDead",true);
        playerMovement.canBeHit = false;
        deadHelper = true;
    }

    private void SetAnimatorBools(bool roll, bool hit, bool hit2, bool stagger) { 
        animatorRoll = roll;
        animatorHit = hit;
        animatorHit2 = hit2;
        animatorStagger = stagger;
    }

/*
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isHit", false);
        if (Input.GetKey("n"))
        {
            animator.SetBool("isHit", true);
        }

        animator.SetBool("isDead", false);
        if (Input.GetKey("x"))
        {
            animator.SetBool("isDead", true);
        }

        SwordAnimations(mouseClicked);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hit") && !animator.GetCurrentAnimatorStateInfo(0).IsName("heavy") && !animator.GetCurrentAnimatorStateInfo(0).IsName("roll"))
        {
            MovementHandler();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("roll")) { 
            // TODO
        }
        
        VelocityHandler();
    }

    void MovementHandler() {
        bool runPressed = Input.GetKey("left shift");
        
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float speed = runPressed ? maxRunVelocity : maxWalkVelocity;

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
            controller.Move(moveDirection * speed * Time.deltaTime);
        } else {
            controller.Move(Vector3.zero);  
        }
        
    }

    void VelocityHandler() {

        velocityZ = controller.velocity.magnitude;

        animator.SetFloat("VelocityZ", velocityZ);
    }


    void SwordAnimations(bool mouseClicked)
    {
        
        // return if no click is registered
        if (!mouseClicked)
        {
            animator.SetBool("isLightHitting", false);
            return;
        }

        bool isLightHitting = false;

        // Light hit
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit") && mouseClicked)
        {
            isLightHitting = true;
        }

        if (mouseClicked) {
            animator.SetBool("isLightHitting", true);
            hit = !hit;
            animator.SetBool("mirrored", hit);
            Debug.Log(hit);
        } ---  
 
    }*/
}
