using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptForInfo : MonoBehaviour
{
    public Text infoText;
    int zombieDmg;
    int zombieHealth;
    int zombieAmt;

    // Start is called before the first frame update
    void Start()
    {
        
        infoText.text = "Damage: TODO\nHealth: TODO\nZombies: " + WorldInfo.enemySpawnAmount;
    }

}
