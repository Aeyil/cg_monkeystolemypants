using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class SpawnHandler : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject enemy;
    InputMaster input;
    public Animator animator;
    float delay;
    float time;
    bool helper;
    bool helper2;


    void Awake()
    {
        input = new InputMaster();
        animator.SetTrigger("fadeIn");
        input.PlayerControls.TestButton.performed += ctx => Trigger(ctx);
        //input.PlayerControls.TestButton2.performed += ctx => nextWave(ctx);

    }

    private void Trigger(InputAction.CallbackContext ctx)
    {
        animator.SetTrigger("fadeOut");

    }

    void Start()
    {
        animator = GameObject.Find("LevelChanger").GetComponent<Animator>();
        time = 0;
        delay = 2.5f;
        spawners = new GameObject[10];
        helper = false;
        helper2 = false;

        //nextWave();
        Debug.Log(WorldInfo.waveNumber);
        Debug.Log(WorldInfo.enemySpawnAmount);
        
        animator.SetTrigger("fadeIn");

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject;
        }

        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (!helper && WorldInfo.enemiesKilled == WorldInfo.enemySpawnAmount) {
            time = Time.time;
            helper = true;
        }
        if (!helper2 && WorldInfo.enemiesKilled == WorldInfo.enemySpawnAmount && WorldInfo.enemySpawnAmount != 0 && Time.time > (time + delay))
        {
            animator.SetTrigger("fadeOut");
            WorldInfo.NextLevel();
            Debug.Log("FADEOUT");
            helper2 = true;
        }
        if (helper2 && Time.time > (time + delay*2)) {
            SceneLoaderInfo.sceneId = 3;
            SceneManager.LoadScene(1);
        }
    }

    private void SpawnEnemy() {
        int randSpawner = Random.Range(0, spawners.Length);
        GameObject zombie = Instantiate(enemy, spawners[randSpawner].transform.position, spawners[randSpawner].transform.rotation);
        zombie.SetActive(true);
    }

    private void StartWave() {
        //WorldInfo.FirstWave();
        for (int i = 0; i < WorldInfo.enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }
    }

    private void nextWave() {

        WorldInfo.NextLevel();
        
        SceneManager.LoadScene("SampleScene");
        animator.SetTrigger("fadeOut");
        
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
