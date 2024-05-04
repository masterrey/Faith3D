using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectableItem : MonoBehaviour
{


    public int score = 1;
    public float rotationSpeed = 100.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.AddScore(score);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    //esta funçao é chamada para adicionar um item coletavel ao menu
    public void AddItemToMenu()
    {
        GameManager.instance.AddItemToMenu(this);
    }
    
}
