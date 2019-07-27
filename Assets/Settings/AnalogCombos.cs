using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class AnalogCombos : MonoBehaviour
{

    public UnityEvent m_OnAnalogGameDone;

    private int[] clockwise = {0,1,2,3,4,5,6,-5,-4,-3,-2,-1};
    private int[] anticlockwise = {0,-1,-2,-3,-4,-5,6,5,4,3,2,1};
    private bool rotDirection;

    public Image progressDisplay;

    bool checkingCombo = false;

    private List<int> inputSequence = new List<int>();

    public float progress = 0;
    public float totalToSucceed = 100;
    public float weight = 5;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            //StartCombo();
        } else if (Input.GetKeyDown(KeyCode.Y)) {
            //StartCombo(false);
        }
        if (checkingCombo) {
            Vector2 inputVector = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (inputVector != Vector2.zero) {

                float angle = Vector2.SignedAngle(Vector2.right, inputVector);

                int SegmentNum = Mathf.RoundToInt(angle) / 30;

                if (inputSequence.Count > 0) {
                    if (inputSequence[inputSequence.Count - 1] != SegmentNum) {
                        inputSequence.Add(SegmentNum);
                    }
                } else {
                    if (SegmentNum == 0)
                        inputSequence.Add(SegmentNum);
                }

                if (inputSequence.Count >= 12) {
                    if (rotDirection)
                        progress += CalculateAccuracy(clockwise) * weight;
                    else 
                        progress += CalculateAccuracy(anticlockwise) * weight;

                    if (progress < totalToSucceed) {
                        inputSequence = new List<int>();
                        
                    } else {
                        checkingCombo = false;
                    }
                    if (progressDisplay)
                        progressDisplay.fillAmount = progress / totalToSucceed;
                }
            }
        }

        if (progressDisplay.GetComponent<Image>().fillAmount >= 1)
        {
            progressDisplay.GetComponent<Image>().fillAmount = 0;
            m_OnAnalogGameDone.Invoke();  
        }
        
    }

    public void StartCombo (bool direction = true) {
        rotDirection = direction;
        checkingCombo = true;
        progress = 0;
        inputSequence.Clear();

        if (progressDisplay) {
            progressDisplay.fillClockwise = !direction;
        }
    }

    private float CalculateAccuracy (int[] template) {
        float score = 0;
        for (int i = 0; i < inputSequence.Count; i++) {
            score += inputSequence[i] == template[i] ? 1 : 0;
        }
        score /= 12;
        return score;        
    }

    public void ChangeGameDoneEvent(UnityEvent gameDone)
    {
        m_OnAnalogGameDone = gameDone;
    }
}
