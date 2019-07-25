using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour {

    public AudioClip pressSound;
    public AudioClip mouseOverSound;

    private UnityEvent m_OnPressed;
    private GameObject _logContent;

    private Choice myChoice;

    // Use this for initialization

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(InvokeOnPressed);
        _logContent = GameObject.Find("LogContent");
    }

    public void SetOnPressedEvent(UnityEvent newPressEvent)
    {
        m_OnPressed = newPressEvent;
    }

    void InvokeOnPressed()
    {

        if (myChoice!=null)
        {
            myChoice.SetSeen();
        }

        AudioManager.PlaySound(pressSound);
        m_OnPressed.Invoke();
    }

    public void PlaySound()
    {
        AudioManager.PlaySound(mouseOverSound);
    }

    public void SetMyChoice(Choice choice)
    {
        myChoice = choice;
    }
}
