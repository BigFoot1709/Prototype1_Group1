using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMoveCamera : MonoBehaviour
{
    public RuntimeAnimatorController myAnimatorController;
    bool tutorialCalled;

    // Start is called before the first frame update
    void Start()
    {
        tutorialCalled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector1 = new Vector2(Input.GetAxis("PS4_RightAnalogHorizontal"), Input.GetAxis("PS4_RightAnalogVertical"));
        if (inputVector1 != Vector2.zero)
        {
            GameObject.Find("TutorialCanvas").GetComponent<TutorialCanvas>().Hide();
            Destroy(this);
        }

        if (tutorialCalled)
        {
            Vector2 inputVector = new Vector2(Input.GetAxis("PS4_RightAnalogHorizontal"), Input.GetAxis("PS4_RightAnalogVertical"));
            if (inputVector != Vector2.zero)
            {
                GameObject.Find("TutorialCanvas").GetComponent<TutorialCanvas>().Hide();
                Destroy(this);
            }
        }
    }

    public void CallAnalogStick()
    {
        GameObject.Find("TutorialCanvas").GetComponent<TutorialCanvas>().CallTutorial(myAnimatorController);
        tutorialCalled = true;
    }
}
