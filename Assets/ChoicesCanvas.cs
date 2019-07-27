using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesCanvas : MonoBehaviour
{
    private bool _showing;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SetChoicesByName("Paperclip");
        }
    }

    public void Hide()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        _showing = false;
    }

    public void Show()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
        _showing = true;
    }

    public bool IsShowing()
    {
        return _showing;
    }

    public OptionEventSetter GetChoiceEventsByName(string choiceName)
    {
        OptionEventSetter[] eventSetters = GameObject.Find("EventContainer").GetComponents<OptionEventSetter>();

        OptionEventSetter returnEvents = null;

        foreach (OptionEventSetter e in eventSetters)
        {
            if (e.GetName().ToUpper().Equals(choiceName.ToUpper()))
            {
                returnEvents = e;
            }
        }

        return returnEvents;
    }

    public void SetChoicesByName (string choiceName)
    {
        GetChoiceEventsByName(choiceName).SetChoices();
    }
}
