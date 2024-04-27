using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookItem : MonoBehaviour
{
    //fild to store the message in rich text format
    [TextAreaAttribute]
    public string bookMessage;

    void Start()
    {
         LegendControl.instance.SetLegend("");
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "Player")
         {
              LegendControl.instance.SetLegend(bookMessage);
             
         }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LegendControl.instance.SetLegend("");
        }
    }

}
