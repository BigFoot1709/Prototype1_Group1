using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XOption : MonoBehaviour
{
    private UnityEvent m_OnOptionChosen;

    // Start is called before the first frame update
    void Start()
    {
        if (m_OnOptionChosen == null)
        {
            m_OnOptionChosen = new UnityEvent();
        }
    }

    // Update is called once per frame
}
