using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerMovement playerMovement;
    public int Health = 100;
    public int Damage = 10;
    public int Armor = 0;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        Health = PlayerInfo.Health;
        Damage = PlayerInfo.Damage;
        Armor = PlayerInfo.Armor;
    }

    public void TakeDamage(int damageTaken){
        if(playerMovement.canBeHit){
            if(Health - damageTaken <= 0){
                Health = 0;
                playerMovement.Die();
            }
            else{
                Health -= damageTaken;
                playerMovement.GetHit();
            }
        }
    }
}
