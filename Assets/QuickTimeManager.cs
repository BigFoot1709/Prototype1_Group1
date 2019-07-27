using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuickTimeManager : MonoBehaviour
{
    //Timer, Icon, Success Event, Fail Event, Manage Combos, Something to call all of this shit

    // Start is called before the first frame update
    public UnityEvent m_OnSuccess;
    public UnityEvent m_OnFailure;

    private bool _QTEInProgress;
    private bool _callQuickTimeEvent;

    void Start()
    {
        Hide();

        if (m_OnSuccess == null)
        {
            m_OnSuccess = new UnityEvent();
        }

        if (m_OnFailure == null)
        {
            m_OnFailure = new UnityEvent();
        }

        _QTEInProgress = false;
        //CallQuickTimeEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (_QTEInProgress)
        {
            QTEIcon qteIcon = GameObject.Find("QTIcon").GetComponent<QTEIcon>();

            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown(qteIcon.GetCurrIcon()))
                {
                    CallSuccess();
                    Debug.Log("SUCCESS SHOULD BE CALLED!");
                }
                else if (Input.anyKeyDown)
                {
                    CallFailure();
                    Debug.Log("FAILURE SHOULD BE CALLED!");
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    CallQuickTimeEvent();
        //}
    }

    public void CallFailure()
    {
        m_OnFailure.Invoke();
        m_OnFailure = new UnityEvent();
        m_OnSuccess = new UnityEvent();
        _QTEInProgress = false;

        Debug.Log("FAILURE CALLED EXTERNALLY");
    }

    public void CallSuccess()
    {
        m_OnSuccess.Invoke();
        print("SUCCESS QTE!! SICCC");
    }

    public void ChangeSuccessEvent(UnityEvent newSuccessEvent)
    {
        m_OnSuccess = newSuccessEvent;
    }

    public void ChangeFailureEvent(UnityEvent newFailureEvent)
    {
        m_OnFailure = newFailureEvent;
    }

    public void CallQuickTimeEvent()
    {
        Show();

        _QTEInProgress = true;

        GameObject.Find("QTIcon").GetComponent<QTEIcon>().RandomiseIcon();

        float randomTime = Random.Range(3, 5);

        Utilities.SearchChild("Timer", this.gameObject).GetComponent<Timer>().StartTimer(randomTime);
    }

    public void Hide()
    {
        for (int i = 0; i < this.transform.childCount;i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        _QTEInProgress = false;
    }

    public void Show()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
