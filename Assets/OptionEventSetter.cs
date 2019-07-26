using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionEventSetter : MonoBehaviour
{
    public string choicesName;

    public string choiceXText;
    public UnityEvent m_OptionX;

    public string choiceOText;
    public UnityEvent m_OptionO;

    public string choiceSquareText;
    public UnityEvent m_OptionSquare;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetChoices()
    {
        GameObject.Find("ChoicesCanvas").GetComponent<ChoicesCanvas>().Show();

        GameObject optionX = GameObject.Find("OptionX");
        GameObject optionO = GameObject.Find("OptionO");
        GameObject optionSquare = GameObject.Find("OptionSquare");

        Utilities.SearchChild("XText", optionX).GetComponent<Text>().text = choiceXText;
        Utilities.SearchChild("CircleText", optionO).GetComponent<Text>().text = choiceOText;
        Utilities.SearchChild("SquareText", optionSquare).GetComponent<Text>().text = choiceSquareText;

        optionX.GetComponent<Option>().ChangeEvent(m_OptionX);
        optionO.GetComponent<Option>().ChangeEvent(m_OptionO);
        optionSquare.GetComponent<Option>().ChangeEvent(m_OptionSquare);
    }

    public string GetName()
    {
        return choicesName;
    }
}
