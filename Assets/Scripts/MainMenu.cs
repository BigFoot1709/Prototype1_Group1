using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   

    public GameObject startDisp;
    public GameObject quitDisp;
    public void StartGame () {
        startDisp.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void QuitGame () {
        quitDisp.SetActive(true);
        Application.Quit();
    }
}
