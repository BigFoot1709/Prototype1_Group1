using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DialogueBox : MonoBehaviour
{

    public int startingNode;
    public float typeTime;

    private int _currNode;
    private List<GameObject> _myChildren = new List<GameObject>();
    private Dictionary<int, MainText> _nodeDictionary = new Dictionary<int, MainText>();
    private int _clickCounter;
    private bool _displayMainText;
    private float _typeTimer;
    private int _charNum;

    private int _switchNode;
    private bool _canControlTextDisplay;
    private string _displayText;

    private bool _textFullyDisplayed;


    // Use this for initialization
    void Start()
    {
        _clickCounter = 2;
        _typeTimer = 0;
        _charNum = 0;
        _canControlTextDisplay = true;

        FindAndSortMainNodes();

        _currNode = startingNode;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            _myChildren.Add(this.transform.GetChild(i).gameObject);
        }

        ChangeNode(_currNode);

        _switchNode = -1;
    }

    private void Update()
    {
        if (_nodeDictionary[_currNode].GetMainText() != null)
        {
            if (_canControlTextDisplay)
            {
                ControlOverallTextDisplay(_displayText);
            }

        }

        //print(MyName.GetName());

        if (IsShowing())
        {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (_clickCounter < 2)
                    {
                        _clickCounter += 1;
                    }

                    if (_clickCounter == 2)
                    {
                        MainText currNode = _nodeDictionary[_currNode];

                        if (currNode.HasChoice() == false)
                        {
                            currNode.InvokeOnClickedEvent();

                            if (currNode.goToConsecutiveNodeOnClick)
                            {
                                ChangeNode(int.Parse(currNode.gameObject.transform.GetChild(0).GetComponent<Text>().text) + 1);
                            }
                        }
                    }
            }
        }
    }

    public void FindAndSortMainNodes()
    {
        foreach (GameObject currentNode in GameObject.FindGameObjectsWithTag("Main"))
        {
            int nodeNum = int.Parse(Utilities.SearchChild("NodeNum", currentNode).GetComponent<Text>().text);
            _nodeDictionary.Add(nodeNum, currentNode.GetComponent<MainText>());
        }
    }

    public void ChangeNode(int nodeNum)
    {

        _currNode = nodeNum;
        MainText currNode = _nodeDictionary[_currNode];

        string myText = _nodeDictionary[_currNode].GetMainText();
        _canControlTextDisplay = false;
        _displayText = myText;
        _canControlTextDisplay = true;

        if (_nodeDictionary.ContainsKey(nodeNum))
        {

                Show();
                currNode.InvokeOnShownEvent();

            if (currNode.HasChoice())
            {
                int numChoices = currNode.GetChoiceList().Count;
                
                for (int i = 0; i < numChoices; i++)
                {
                    GameObject currChoice = Utilities.SearchChild("ChoiceButton" + i, this.gameObject);
                    ModifyChoice(i, currChoice, currNode);
                }
            }

            foreach (GameObject currChild in _myChildren)
            {
                if (currChild.name.Equals("CurrentCharacterText"))
                {
                        currChild.GetComponent<Text>().text = currNode.GetCurrCharacter();
                }

                if (currChild.name.Equals("MainText"))
                {
                    currChild.GetComponent<Text>().text = "";
                    _displayMainText = true;
                }
            }
           
        }
    }

    public void Show()
    {
        int numChoices = _nodeDictionary[_currNode].GetChoiceList().Count;

        if (_nodeDictionary[_currNode].HasChoice())
        {
            for (int i = 0; i < numChoices; i++)
            {
                GameObject currChoice = Utilities.SearchChild("ChoiceButton" + i, this.gameObject);
                currChoice.SetActive(true);
            }
        }

        foreach (GameObject currChild in _myChildren)
        {
            if (!currChild.name.Contains("Choice"))
            {
                currChild.SetActive(true);
            } else if (!_nodeDictionary[_currNode].HasChoice())
            {
                currChild.SetActive(false);
            }

        }
    }

    public void Hide()
    {
        Utilities.SearchChild("BackgroundImage", this.gameObject).GetComponent<Image>().enabled = true;
        Utilities.SearchChild("CurrentCharacterImage", this.gameObject).GetComponent<Image>().enabled = true;
        Utilities.SearchChild("CurrentCharacterText", this.gameObject).GetComponent<Text>().enabled = true;

        foreach (GameObject currChild in _myChildren)
        {
            currChild.SetActive(false);
        }

        _clickCounter = 0;
    }

    private void ModifyChoice(int choiceNum, GameObject choiceGO, MainText currentNode)
    {
        Choice currChoice = currentNode.GetChoiceList()[choiceNum];

        choiceGO.transform.GetChild(0).GetComponent<Text>().text = currChoice.GetChoiceText();
        choiceGO.GetComponent<ChoiceButton>().SetOnPressedEvent(currChoice.GetOnChosenEvent());
        choiceGO.GetComponent<ChoiceButton>().SetMyChoice(currChoice);

        if (currChoice.CheckSeen())
        {
            choiceGO.GetComponent<Image>().color = Color.cyan;
        } else
        {
            choiceGO.GetComponent<Image>().color = Color.white;
        }
    }

    public bool IsShowing()
    {
        bool visible = false;

        foreach (GameObject currChild in _myChildren)
        {
            if (currChild.activeInHierarchy)
            {
                visible = true;
            }
        }

        return visible;
    }

    private void ControlMainTextDisplay(string displayText)
    {

            if (_displayMainText)
            {
                GameObject mainTextButton = Utilities.SearchChild("MainText", this.gameObject);

                _typeTimer += Time.deltaTime;

                string mainText = mainTextButton.GetComponent<Text>().text;

                List<char> myChars = displayText.ToCharArray().ToList<char>();

                if (myChars != null)
                {
                    if (_typeTimer >= typeTime)
                    {
                        mainText += myChars[_charNum].ToString();
                        mainTextButton.GetComponent<Text>().text = mainText;
                        _charNum++;
                        _typeTimer = 0.0f;
                    }

                    if (_charNum == myChars.Count)
                    {
                        _textFullyDisplayed = true;
                        _displayMainText = false;
                        _typeTimer = 0.0f;
                        _charNum = 0;
                    }
                    else
                    {
                        _textFullyDisplayed = false;
                    }

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        _textFullyDisplayed = true;
                        _clickCounter = 0;
                        mainTextButton.GetComponent<Text>().text = displayText;
                        _displayMainText = false;
                        _typeTimer = 0.0f;
                        _charNum = 0;
                    }
                }
        }
    }

    public void SwitchNode()
    {

        if (_switchNode != -1)
        {
            ChangeNode(_switchNode);
            Show();

        } else
        {
            Hide();
        }

        _switchNode = -1;
    }

    public void SetSwitchNode(int num)
    {
        _switchNode = num;
    }

    private void ControlOverallTextDisplay(string myText)
    {
        if (myText == "")
        {
            Utilities.SearchChild("BackgroundImage", this.gameObject).GetComponent<Image>().enabled = false;
            Utilities.SearchChild("CurrentCharacterImage", this.gameObject).GetComponent<Image>().enabled = false;
            Utilities.SearchChild("CurrentCharacterText", this.gameObject).GetComponent<Text>().enabled = false;
        }
        else
        {
            Utilities.SearchChild("BackgroundImage", this.gameObject).GetComponent<Image>().enabled = true;
            Utilities.SearchChild("CurrentCharacterImage", this.gameObject).GetComponent<Image>().enabled = true;
            Utilities.SearchChild("CurrentCharacterText", this.gameObject).GetComponent<Text>().enabled = true;

            ControlMainTextDisplay(myText);
        }
    }

    public void ChangeCurrNodeEvent(UnityEvent newOnClickedEvent)
    {
        if (IsShowing())
        {
            _nodeDictionary[_currNode].GetComponent<MainText>().ChangeOnClickedEvent(newOnClickedEvent);
        }
    }

    public bool MainTextIsFullyDisplayed()
    {
        return _textFullyDisplayed;
    }
}
