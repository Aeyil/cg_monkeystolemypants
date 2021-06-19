using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;
    [SerializeField] Transform camera;
    // Start is called before the first frame update

    InputMaster input;

    float turnSmoothVelocity;
    Vector3 runSmoothVelocity;
    public float runSmoothTime = 1.0f;
    public float turnSmoothTime = 0.1f;

    Vector2 movementInput;
    Vector3 currentMovement;
    [SerializeField] float speed = 3f;
    [SerializeField] float sprintSpeed= 5f;
    [SerializeField] float rollSpeed = 15f;

    public bool isMoving;
    public bool isSprinting;
    public bool isRolling;

    PlayerAnimationHandler animationHandler;

    void Awake() {
        input = new InputMaster();

        input.PlayerControls.Movement.performed += ctx => handleMovementInput(ctx);
        input.PlayerControls.Movement.canceled += ctx => handleMovementInput(ctx);
        input.PlayerControls.Run.performed += ctx => handleSprinting(ctx);
        input.PlayerControls.Roll.performed += ctx => handleRolling(ctx);
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animationHandler = GetComponent<PlayerAnimationHandler>();
    }

    void Update() {
        move();
        Debug.Log(isSprinting);
    }

    public void handleRolling(InputAction.CallbackContext value) {
        if (isMoving)
        {
            isRolling = true;
            animationHandler.StartRoll();
        }
    }

    public void handleMovementInput(InputAction.CallbackContext value){
        movementInput = value.ReadValue<Vector2>();
        isMoving = movementInput.x != 0 || movementInput.y != 0;
    }

    public void handleSprinting(InputAction.CallbackContext value){
        isSprinting = !isSprinting;
    }
   

    public void move(){
       

        if (isMoving && !isRolling) {
            float targetAngle = Mathf.Atan2(movementInput.x, movementInput.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            currentMovement = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
            float appliedSpeed = isSprinting ? sprintSpeed : speed;
            controller.Move(currentMovement * appliedSpeed * Time.deltaTime);
            animationHandler.velocity = controller.velocity.magnitude;
        }
        else if (isRolling) {
            transform.LookAt(currentMovement + transform.position);
            controller.Move(currentMovement * rollSpeed * Time.deltaTime);
            animationHandler.velocity = controller.velocity.magnitude;

        }
        else {
            currentMovement = Vector3.zero;
            animationHandler.velocity = 0;
        }
    }

    private void OnEnable() {
        input.PlayerControls.Enable();
    }

    private void OnDisable() {
        input.PlayerControls.Disable();
    }
}
