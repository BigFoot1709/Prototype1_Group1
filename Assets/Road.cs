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
        Vector3 targetPos = this.transform.position;
        targetPos.z = GameObject.Find("SwitchMarker").transform.position.z;
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, 0.1f * Time.deltaTime);
    }
}
