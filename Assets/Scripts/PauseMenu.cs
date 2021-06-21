using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject firstSelected;
    public PlayerMovement player;
    InputMaster input;
    bool isMenuOpen;
    // Start is called before the first frame update
    void Awake() {
        input = new InputMaster();

        input.PlayerControls.Pause.performed += ctx => PauseGame();
        input.MenuControls.Back.performed += ctx => ResumeGame();
        input.PlayerControls.Enable();
        input.MenuControls.Disable();
        pauseMenu.SetActive(false);
    }

    void Start()
    {
        isMenuOpen = false;
    }

    public void PauseGame(){
        input.PlayerControls.Disable();
        input.MenuControls.Enable();
        player.toggleInput();
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelected);
        isMenuOpen = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
        Debug.Log("TES");
        input.PlayerControls.Enable();
        input.MenuControls.Disable();
        player.toggleInput();
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        isMenuOpen = false;
        Time.timeScale = 1f;
    }

    public void ExitGame(){
        Application.Quit();
    }

    private void OnEnable() {
        if(isMenuOpen){
            input.PlayerControls.Disable();
            input.MenuControls.Enable();
        }
        else{
            input.PlayerControls.Enable();
            input.MenuControls.Disable();
        }
    }

    private void OnDisable() {
        input.PlayerControls.Disable();
        input.MenuControls.Disable();
    }
}
