using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<ColectableItem> colectableItems = new List<ColectableItem>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        Debug.Log("Score: " + score);
    }

    public void AddItemToMenu(ColectableItem item)
    {
        colectableItems.Add(item);
    }
   
}
