using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuGame : MonoBehaviour
{

    //singleton 
    public static MenuGame instance;

    public int faith = 100;
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

    void Start()
    {
        DecreaseFaith(0);
    }
    public GameObject pauseMenu;
    public TextMeshProUGUI faithText;

    public void DecreaseFaith(int dec)
    {

        faith = faith - dec; 
        faith = Mathf.Clamp(faith, 0, 100);
        
        faithText.text = "Faith: " + faith + "%";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Time.timeScale = 1;
        Application.LoadLevel("Menu");
    }


}
