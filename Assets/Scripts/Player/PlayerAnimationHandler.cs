using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoD_Animationcontroller : MonoBehaviour
{

    Animator animator;
    public float velocityZ = 0.0f;
    public float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 2f;
    float distance  = 5;
    bool hit = false;
    CharacterController controller;
    [SerializeField] Transform camera;
    float turnSmoothVelocity;
    Vector3 runSmoothVelocity;
    public float runSmoothTime = 1.0f;
    public float turnSmoothTime = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool mouseClicked = Input.GetMouseButtonDown(0);
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");
        bool backPressed = Input.GetKey("s");

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
        
        VelocityHandler(forwardPressed, leftPressed, rightPressed, runPressed, backPressed);
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

    void VelocityHandler(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, bool backPressed) {

     
        velocityZ = controller.velocity.magnitude;


        /*if ((forwardPressed || leftPressed || rightPressed || backPressed) && velocityZ < currentMaxVelocity) {
            velocityZ = controller.velocity.magnitude + Time.deltaTime * acceleration;
        }

        if (!(forwardPressed || leftPressed || rightPressed || backPressed) && velocityZ > 0.0f) {
            velocityZ = controller.velocity.magnitude - Time.deltaTime * acceleration;
        }

        if (!(forwardPressed || leftPressed || rightPressed || backPressed) && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }*/
        
       
        /*

        if (backPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
            Debug.Log("VelZ");
            Debug.Log(velocityZ);
            
        }

        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }


        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        // increase velocity right
        // reset Z
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        // decrease VelocityZ

        if (!forwardPressed && !backPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        // reset VelocityZ
        if (!forwardPressed && !backPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }

        // increase VelX if left not pressed
        if (!leftPressed && !backPressed  && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        // decrease velX if right not pressed
        if (!rightPressed && !backPressed  && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        // reset velX
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }
        
        // lock forward
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }

        
        */
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
        }
 
    }
}
