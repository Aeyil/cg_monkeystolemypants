using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject ControlsMenu;
    [SerializeField] GameObject CreditsMenu;

    [SerializeField] GameObject MainMenuFirstButton;
    [SerializeField] GameObject ControlsMenuFirstButton;
    [SerializeField] GameObject CreditsMenuFirstButton;

    public void startGame(){
        PlayerInfo.initialize();
        SceneLoaderInfo.sceneId = 2;
        SceneManager.LoadScene(1);
        Debug.Log("Starting game.");
    }

    public void exitGame(){
        Debug.Log("Exiting game.");
        Application.Quit();
    }

    public void SwtichToMainMenu(){
        EnableMenus(true,false,false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(MainMenuFirstButton);
    }

    public void SwtichToControlsMenu(){
        EnableMenus(false,true,false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ControlsMenuFirstButton);
    }

    public void SwtichToCreditsMenu(){
        EnableMenus(false,false,true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(CreditsMenuFirstButton);
    }

    void EnableMenus(bool main, bool controls, bool credits){
        MainMenu.SetActive(main);
        ControlsMenu.SetActive(controls);
        CreditsMenu.SetActive(credits);
    }
}
