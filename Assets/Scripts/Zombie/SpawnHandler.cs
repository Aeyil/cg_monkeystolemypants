using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SpawnHandler : MonoBehaviour
{
    private int waveNumber = 0;
    private int enemySpawnAmount = 0;
    private int enemiesKilled = 0;

    public GameObject[] spawners;
    public GameObject enemy;
    InputMaster input;

    void Awake()
    {
        input = new InputMaster();

        input.PlayerControls.TestButton.performed += ctx => StartWave(ctx);
        input.PlayerControls.TestButton2.performed += ctx => killEnemies(ctx);

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
        
    }

    private void SpawnEnemy() {

        int randSpawner = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[randSpawner].transform.position, spawners[randSpawner].transform.rotation);
    }

    private void StartWave(InputAction.CallbackContext value) {
        waveNumber = 1;
        enemySpawnAmount = 2;
        enemiesKilled = 0;

        for (int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }
    }

    private void nextWave() {
        waveNumber++;
        enemySpawnAmount += 2;
        enemiesKilled = 0;

        for (int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }
    }

    private void killEnemies(InputAction.CallbackContext value) {
        // aint working
        for (int i = 0; i <= enemySpawnAmount; i++)
        {
            Destroy(enemy);
        }
        nextWave();
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
