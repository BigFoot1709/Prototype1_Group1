using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeAxeEvent : MonoBehaviour
{
    public UnityEvent m_ChangeEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (m_ChangeEvent == null)
        {
            m_ChangeEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Handcuffs") == null)
        {
            this.GetComponent<InteractableObject>().ChangeOnZoomFinishedEvent(m_ChangeEvent);
            Destroy(this);
        }
    }
}
