using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * 100* Time.deltaTime;

        if (this.transform.position.z >= GameObject.Find("BeginningMarker").transform.position.z)
        {
            Vector3 myPos = this.transform.position;

            if (this.gameObject.name.Equals("Road2"))
            {
                GameObject endMarker = Utilities.SearchChild("EndMarker", GameObject.Find("Road1"));
                myPos.z = endMarker.transform.position.z;
            } else
            {
                GameObject endMarker = Utilities.SearchChild("EndMarker", GameObject.Find("Road2"));
                myPos.z = endMarker.transform.position.z;
            }
            this.transform.position = myPos;
        }
    }
}
