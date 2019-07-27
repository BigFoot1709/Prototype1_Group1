using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeHandcuffQTEvent : MonoBehaviour
{
    public int numberOfCombos;
    public UnityEvent m_OnSuccessfulCombos;

    private int _currNumberOfCombos;

    private void Start()
    {
        _currNumberOfCombos = 0;

        if (m_OnSuccessfulCombos == null)
        {
            m_OnSuccessfulCombos = new UnityEvent();
        }
    }

    public void CheckHandcuffSuccess()
    {
        _currNumberOfCombos++;
        Debug.Log("CURRNUMCOMBOS: "+ _currNumberOfCombos);

        if (_currNumberOfCombos >= numberOfCombos-1)
        {
            Debug.Log("COMPLETED ALL QTEs");
            GameObject.Find("QTECanvas").GetComponent<QuickTimeManager>().ChangeSuccessEvent(m_OnSuccessfulCombos);
        }
    }
}
