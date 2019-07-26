using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Option : MonoBehaviour
{
    public KeyCode myKeyCode;

    private UnityEvent m_OnOptionChosen;

    // Start is called before the first frame update
    void Start()
    {
        if (m_OnOptionChosen == null)
        {
            m_OnOptionChosen = new UnityEvent();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(myKeyCode))
        {
            if (GameObject.Find("ChoicesCanvas").GetComponent<ChoicesCanvas>().IsShowing())
            {
                m_OnOptionChosen.Invoke();
            }
        }
    }

    public void ChangeEvent(UnityEvent optionChosen)
    {
        m_OnOptionChosen = optionChosen;
    }
}
