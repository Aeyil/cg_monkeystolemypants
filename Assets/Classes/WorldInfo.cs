using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldInfo
{
    public static int enemiesKilled = 0;
    public static int enemySpawnAmount = 2;
    public static int waveNumber = 1;
    public static int zombieHealth = 0;
    public static int zombieDamage = 0;

    public static void Initialize() {

        enemiesKilled = 0;
        enemySpawnAmount = 2;
        waveNumber = 1;
        zombieHealth = 25;
        zombieDamage = 5;
    }

    public static void NextLevel() {
        waveNumber++;
        enemySpawnAmount += 5;
        enemiesKilled = 0;
        zombieHealth += 10;
        zombieDamage += 5;
    }

    public static void FirstWave() {
        waveNumber = 1;
        enemySpawnAmount = 2;
        enemiesKilled = 0;
    }
}
