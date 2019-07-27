using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToBeContinued : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if (this.GetComponent<Image>().color.a > 0)
        {
            GameObject.Find("ChoicesCanvas").GetComponent<ChoicesCanvas>().Hide();
        }
    }
}
