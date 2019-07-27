using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Option : MonoBehaviour
{
    public KeyCode myKeyCode;

    private UnityEvent m_OnOptionChosen;
    public UnityEvent m_OnOptionChosenDefault;

    // Start is called before the first frame update
    void Start()
    {
        if (m_OnOptionChosen == null)
        {
            m_OnOptionChosen = new UnityEvent();
        }

        if (m_OnOptionChosenDefault == null)
        {
            m_OnOptionChosenDefault = new UnityEvent();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(myKeyCode))
        {
            if (GameObject.Find("ChoicesCanvas").GetComponent<ChoicesCanvas>().IsShowing())
            {
                if (GameObject.Find("ToBeContinuedImage").GetComponent<Image>().color.a <= 0)
                {
                    m_OnOptionChosenDefault.Invoke();
                }
                m_OnOptionChosen.Invoke();
            }
        }
    }

    public void ChangeEvent(UnityEvent optionChosen)
    {
        m_OnOptionChosen = optionChosen;
    }
}
