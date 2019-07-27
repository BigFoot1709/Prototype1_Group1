using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeAnalogGameEvent : MonoBehaviour
{
    public UnityEvent m_ChangeAnalogueGameDoneEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (m_ChangeAnalogueGameDoneEvent == null)
        {
            m_ChangeAnalogueGameDoneEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeAnalogEndEvent()
    {
        GameObject.Find("AnalogCanvas").GetComponent<AnalogCombos>().ChangeGameDoneEvent(m_ChangeAnalogueGameDoneEvent);
    }
}
