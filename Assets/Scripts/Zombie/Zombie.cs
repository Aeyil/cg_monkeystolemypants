using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int Health = 25;
    public int Damage = 5;
    ZombieAI zombieAi;
    SpawnHandler spawnHandler;
    void Start()
    {
        zombieAi = GetComponent<ZombieAI>();
        spawnHandler = GetComponent<SpawnHandler>();
    }

    public void GetHit(int damageTaken){
        if(zombieAi.aiState != ZombieAI.AIState.dead){
            if (Health - damageTaken <= 0)
            {
                Health = 0;
                zombieAi.StartDead();
                WorldInfo.enemiesKilled++;
                Debug.Log("Zombie killed");
            }
            else
            {
                Health -= damageTaken;
                zombieAi.StartStagger();
            }
        }
    }
}
