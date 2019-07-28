using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDialogueStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Subtitles").GetComponent<Subtitles>().PlayConversation("Opening Dialogue");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("_VoiceSource").GetComponent<AudioSource>().isPlaying)
        {
            if (GameObject.Find("Director") != null)
            {
                Destroy(GameObject.Find("Director"));
            }

            if (GameObject.Find("Subtitles")!=null)
            {
                Destroy(GameObject.Find("Subtitles"));
            }
        }
    }
}
