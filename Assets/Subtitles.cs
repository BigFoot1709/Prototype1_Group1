using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour
{
    private List<string> _currLines;
    private int _index;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = "";
        _index = 0;
        //_currConversation = GameObject.Find("ConversationHolder").GetComponent<Conversation>().GetLines();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayConversation("Harry Potter");
        }
    }

    public void NextLine()
    {
        this.GetComponent<Text>().text = _currLines[_index];
        if (_index < _currLines.Count)
        {
            _index++;
        }
    }

    public void SetCurrConversation(List<string> newConvo)
    {
        _currLines = newConvo;
    }

    public void Empty()
    {

    }

    public void PlayConversation(string name)
    {

        Conversation currentConversation = GetConversationByName(name);
        _currLines = currentConversation.GetLines();

        GameObject.Find("DialogueSoundSource").GetComponent<DialogueSoundSource>().PlayDialogue(currentConversation.GetClip(), currentConversation.GetAnimator());
    }

    public Conversation GetConversationByName(string name)
    {
        Conversation[] conversations = GameObject.Find("ConversationHolder").GetComponents<Conversation>();

        Conversation returnConvo = null;

        foreach (Conversation c in conversations)
        {
            if (c.GetName().ToUpper().Contains(name.ToUpper()))
            {
                returnConvo = c;
            }
        }

        return returnConvo;
    }
}
