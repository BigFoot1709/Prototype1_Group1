using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCallTutorial : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Subtitles").GetComponent<Subtitles>().PlayConversation("KeatonTestConvo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
