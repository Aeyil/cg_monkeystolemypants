using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptForHeadline : MonoBehaviour
{
    public Text headline;
    // Start is called before the first frame update
    void Start()
    {
        headline.text = "Round: " + WorldInfo.waveNumber.ToString();
    }

   
}
