using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuttingControl : MonoBehaviour
{
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
