using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            SceneLoaderInfo.sceneId = 3;
            SceneManager.LoadScene(1);
        }
    }
}
