using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInfo
{
    public static int Health;
    public static int Damage;
    public static int Armor;
    public static int Coins;

    public static void initialize(){
        Health = 100;
        Damage = 10;
        Armor = 0;
        Coins = 0;
    }
}
