using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{

    public float fadeAmount;
    private bool _fadeIn;
    private bool _fadeOut;
    private string _nextScene;

    // Use this for initialization
    void Start()
    {
        CanvasGroup fadeCanvas = this.GetComponent<CanvasGroup>();
        fadeCanvas.alpha = 1;
        OpenScene();
        fadeAmount = 0.05f;
    }

    private void Update()
    {
        CanvasGroup fadeCanvas = this.GetComponent<CanvasGroup>();

        if (_fadeIn)
        {
            if (fadeCanvas.alpha > 0)
            {
                fadeCanvas.alpha -= fadeAmount;

            } else
            {
                this.gameObject.SetActive(false);
                _fadeIn = false;
            }
        } else if (_fadeOut)
        {
            if (fadeCanvas.alpha < 1)
            {
                fadeCanvas.alpha += fadeAmount;
            }
            else
            {
                _fadeOut = false;
                SceneManager.LoadScene(_nextScene);
            }

        }
    }

    public void ChangeScene(string sceneName)
    {
        this.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        _fadeOut = true;
        _nextScene = sceneName;
    }

    private void OpenScene()
    {
        _fadeIn = true;
    }

}
