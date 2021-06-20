using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SpawnHandler : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject enemy;
    InputMaster input;


    void Awake()
    {
        input = new InputMaster();

        input.PlayerControls.TestButton.performed += ctx => StartWave(ctx);

    }
    void Start()
    {
        spawners = new GameObject[5];
        

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (WorldInfo.enemiesKilled == WorldInfo.enemySpawnAmount && WorldInfo.enemySpawnAmount != 0)
        {
            nextWave();
        }
    }

    private void SpawnEnemy() {

        int randSpawner = Random.Range(0, spawners.Length);
        GameObject zombie = Instantiate(enemy, spawners[randSpawner].transform.position, spawners[randSpawner].transform.rotation);
        zombie.SetActive(true);
    }

    private void StartWave(InputAction.CallbackContext value) {
        WorldInfo.FirstWave();
        for (int i = 0; i < WorldInfo.enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }
    }

    private void nextWave() {
        WorldInfo.NextLevel();
        for (int i = 0; i < WorldInfo.enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }
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
