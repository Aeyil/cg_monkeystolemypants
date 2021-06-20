using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    // This class contains a delay to loading for presentation purposes.

    [SerializeField] Slider loadingBar;

    bool isLoading;
    float startTime;
    float delay = 0.5f;

    void Start()
    {
        isLoading = false;
        loadingBar.value = 0f;
        startTime = Time.time;
    }

    void Update() {
        if(!isLoading && Time.time > startTime + delay){
            StartCoroutine(loadScene(SceneLoaderInfo.sceneId));
            isLoading = true;
        }
    }

    IEnumerator loadScene(int sceneIndex){
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneIndex);
        while(!loading.isDone){
            loadingBar.value = Mathf.Clamp01(loading.progress / 0.9f);

            yield return null;
        }
    }
}
