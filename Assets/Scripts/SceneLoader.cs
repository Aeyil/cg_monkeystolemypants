using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] Slider loadingBar;

    void Start()
    {
        loadingBar.value = 0f;
        StartCoroutine(loadScene(SceneLoaderInfo.sceneId));
    }

    IEnumerator loadScene(int sceneIndex){
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneIndex);

        while(!loading.isDone){
            loadingBar.value = Mathf.Clamp01(loading.progress / 0.9f);

            yield return null;
        }
    }
}
