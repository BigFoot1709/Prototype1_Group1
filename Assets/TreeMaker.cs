using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMaker : MonoBehaviour
{

    public GameObject tree;
    public int gap;
    public int treeNum;

    private float _zPos;

    // Start is called before the first frame update
    void Start()
    {
        _zPos = this.transform.position.z;

        for (int i = 0; i < treeNum; i++)
        {
            Vector3 pos = this.transform.position;
            _zPos += gap;
            pos.z = _zPos;
            Instantiate(tree, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
