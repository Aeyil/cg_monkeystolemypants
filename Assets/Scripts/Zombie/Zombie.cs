using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int Health = 25;
    public int Damage = 5;
    ZombieAI zombieAi;
    void Start()
    {
        zombieAi = GetComponent<ZombieAI>();
    }

    public void GetHit(int damageTaken){
        if(!zombieAi.isDead){
            if (Health - damageTaken <= 0)
            {
                Health = 0;
                zombieAi.StartDie();
            }
            else
            {
                Health -= damageTaken;
                zombieAi.StartStagger();
            }
        }
    }
}
