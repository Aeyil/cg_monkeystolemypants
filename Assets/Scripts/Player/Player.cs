using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int Health = 100;
    public int Damage = 10;
    public int Armor = 0;
    void Start()
    {
        Health = PlayerInfo.Health;
        Damage = PlayerInfo.Damage;
        Armor = PlayerInfo.Armor;
    }
}
