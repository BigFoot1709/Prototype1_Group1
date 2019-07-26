using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour
{

    public float originalFieldOfView;
    public float speed;
    public float offset;

    private Transform _zoomObject;
    private bool _startZoom;

    private GameObject _freeLookCamera;

    // Start is called before the first frame update
    void Start()
    {
        originalFieldOfView = this.GetComponent<Camera>().fieldOfView;
        _startZoom = false;
        _freeLookCamera = GameObject.Find("FreeLookCameraRig");
    }

    // Update is called once per frame
    void Update()
    {
        if (_startZoom)
        {
            //this.transform.position = Vector3.MoveTowards(this.transform.position, _zoomObject.position, speed * Time.deltaTime);
            Vector3 targetPos = _zoomObject.position;
            targetPos.z -= offset;
            targetPos.y -= offset;

            this.transform.position = targetPos;

            if (this.GetComponent<Camera>().fieldOfView > 30)
            {
                this.GetComponent<Camera>().fieldOfView -= 0.1f;
            } else
            {
                _startZoom = false;
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            ReturnToMainView();
        }
    }

    public void FocusOnObject(Transform newZoomObject)
    {
        //this.transform.position = GameObject.Find("FreeLookCameraRig").transform.position;
        _zoomObject = newZoomObject;
        _startZoom = true;
    }

    public void ReturnToMainView()
    {
        if (this.GetComponent<Camera>().enabled)
        {
            _freeLookCamera.SetActive(true);
            this.GetComponent<Camera>().fieldOfView = originalFieldOfView;
            this.GetComponent<Camera>().enabled = false;
        }
    }

}
