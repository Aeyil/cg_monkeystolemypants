using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


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
}
