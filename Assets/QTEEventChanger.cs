using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QTEEventChanger : MonoBehaviour
{
    public UnityEvent m_ChangeSuccessEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (m_ChangeSuccessEvent == null)
        {
            m_ChangeSuccessEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSuccessEvent()
    {
        GameObject.Find("QTECanvas").GetComponent<QuickTimeManager>().ChangeSuccessEvent(m_ChangeSuccessEvent);
    }
}
