using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class LevelChangerScript : MonoBehaviour
{

    InputMaster input;
    public Animator animator;
    
    void Awake()
    {
        input = new InputMaster();

        //input.PlayerControls.TestButton2.performed += ctx => FadeToLevel(1, ctx);

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel(int levelIndex, InputAction.CallbackContext value) {
        animator.SetTrigger("fadeOut");
        Debug.Log("FADEPLS");
    }

    private void OnEnable()
    {
        input.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        input.PlayerControls.Disable();
    }
}
