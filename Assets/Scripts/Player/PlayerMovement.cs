using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;
    PlayerAnimationHandler animationHandler;
    Player player;
    [SerializeField] Transform camera;
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
    public bool isAttacking;
    public bool canAct;
    public bool canBeHit;

    float damageTimeStart;
    float damageTimeOffset = 0.5f;


    void Awake() {
        input = new InputMaster();

        input.PlayerControls.Movement.performed += ctx => HandleMovementInput(ctx);
        input.PlayerControls.Movement.canceled += ctx => HandleMovementInput(ctx);
        input.PlayerControls.Run.performed += ctx => HandleSprinting(ctx);
        input.PlayerControls.Roll.performed += ctx => HandleRolling(ctx);
        input.PlayerControls.Attack.performed += ctx => HandleAttack(ctx);
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animationHandler = GetComponent<PlayerAnimationHandler>();
        player = GetComponent<Player>();
        canAct = true;
        canBeHit = true;
    }

    void Update() {
        Move();
    }

    public void HandleRolling(InputAction.CallbackContext value) {
        if (isMoving && canAct)
        {
            isRolling = true;
            canAct = false;
            canBeHit = false;
            animationHandler.StartRoll();
        }
    }

    public void HandleMovementInput(InputAction.CallbackContext value){
        movementInput = value.ReadValue<Vector2>();
        isMoving = movementInput.x != 0 || movementInput.y != 0;
    }

    public void HandleSprinting(InputAction.CallbackContext value){
        isSprinting = !isSprinting;
    }

    public void HandleAttack(InputAction.CallbackContext value){
        if(canAct){
            isAttacking = true;
            canAct = false;
            animationHandler.StartAttack();
        }
    }
   
    public void GetHit(int damageTaken){
        if(!isRolling && canBeHit){
            isAttacking = false;
            canAct = false;
            animationHandler.StartStagger();
            player.TakeDamage(damageTaken);
        }
    }

    public void Die(){
        if(canBeHit){
            isAttacking = false;
            canAct = false;
            animationHandler.StartDeath();
        }
    }

    public void checkAttackTargets(){
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + new Vector3(-1,0,0), transform.localScale / 2, Quaternion.identity);
        for(int i = 0; i < hitColliders.Length; i++){
            if(hitColliders[i].tag == "Enemy"){
                hitColliders[i].GetComponent<Zombie>().GetHit(player.Damage);
            }
        }
    }

    public void Move(){
        if (isMoving && !isRolling && !isAttacking && canAct) {
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
