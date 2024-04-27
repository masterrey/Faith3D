using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LegendControl : MonoBehaviour
{

    public TextMeshProUGUI legendText;

    //singleton
    public static LegendControl instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetLegend(string legend)
    {
        legendText.text = legend;
    }

   
}
