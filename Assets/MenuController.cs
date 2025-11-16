using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject tutorial;

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.C))
        {
            tutorial.SetActive(false);
        }
    }
   public void LoadGame()
    {
        SceneManager.LoadScene(1);
        
    }

     public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        
    }

    public void Tutorial()
    {
        tutorial.SetActive(true);
    }

}
