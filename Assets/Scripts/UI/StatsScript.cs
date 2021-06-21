using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class StatsScript : MonoBehaviour
{
    public TextMeshProUGUI stats;
    // Start is called before the first frame update
    void Start()
    {
        stats.text = "Wave Reached: " + WorldInfo.waveNumber.ToString();
    }
}
