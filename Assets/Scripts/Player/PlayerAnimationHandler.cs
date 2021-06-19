using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{

    Animator animator;
    public float velocity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetFloat("Velocity", velocity);
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
