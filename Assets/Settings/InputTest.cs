using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    private bool Combo = false;
    private float ComboRate = 1f;

    private bool CR_Running = false;

    private int[] sequence = {0, 3, 0, 1};
    private int index = 0;

    private void Update() {
        if (Input.GetButton("Submit")) {
            Combo = true;
            index = 0;
        }

        if (Combo) {
            if (CR_Running) {
                switch (sequence[index]) {
                    case 0: 
                    if (Input.GetAxis("Horizontal") > 0.5f) {
                        Debug.Log("Right");
                        StopCoroutine(StageInCombo());
                        index++;
                    }
                    break;
                    case 1: 
                    if (Input.GetAxis("Vertical") > 0.5f) {
                        Debug.Log("Up");
                        StopCoroutine(StageInCombo());
                        index++;
                    }
                    break;
                    case 2: 
                    if (Input.GetAxis("Horizontal") < -0.5f) {
                        Debug.Log("Left");
                        StopCoroutine(StageInCombo());
                        index++;
                    }
                    break;
                    case 3: 
                    if (Input.GetAxis("Vertical") < -0.5f) {
                        Debug.Log("Down");
                        StopCoroutine(StageInCombo());
                        index++;
                    }
                    break;
                }

                if (index >= sequence.Length) {
                    Combo = false;
                    Debug.Log("Success");
                }
            } else {
                StartCoroutine(StageInCombo());
            }
        }

    }

    IEnumerator StageInCombo () {
        CR_Running = true;
        int currPoint = index;
        yield return new WaitForSeconds(ComboRate);
        if (index == currPoint) {
            Combo = false;
            Debug.Log("Fail");
        }
        CR_Running = false;
    }
}
