using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour
{

    private float _originalFieldOfView;
    public float speed;
    public float rotateSpeed;
    public float offset;
    public float maxZoom;

    private Transform _zoomObject;
    private bool _startZoom;

    private GameObject _freeLookCamera;
    private Transform _originalTransform;

    // Start is called before the first frame update
    void Start()
    {
        _originalFieldOfView = this.GetComponent<Camera>().fieldOfView;
        _originalTransform = this.GetComponent<Transform>();
        _startZoom = false;
        _freeLookCamera = GameObject.Find("FreeLookCameraRig");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_startZoom)
        {
            Vector3 targetPos = _zoomObject.position;
            targetPos.z -= offset;
            targetPos.y -= offset;

            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);

            Vector3 targetDir = _zoomObject.position - transform.position;

            // The step size is equal to speed times frame time.
            float step = rotateSpeed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            //Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(newDir), Time.deltaTime * rotateSpeed);

            if (this.GetComponent<Camera>().fieldOfView > maxZoom)
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

       GameObject mainCamera =  _freeLookCamera.transform.GetChild(0).transform.GetChild(0).gameObject;
        mainCamera.transform.parent = null;

        this.transform.localPosition = _freeLookCamera.transform.localPosition;
        this.transform.rotation = mainCamera.transform.rotation;
        this.transform.localScale = _freeLookCamera.transform.localScale;

        mainCamera.transform.SetParent(_freeLookCamera.transform.GetChild(0));
        
        _zoomObject = newZoomObject;
        _startZoom = true;
    }

    public void ReturnToMainView()
    {
        if (this.GetComponent<Camera>().enabled)
        {
            _freeLookCamera.SetActive(true);
            this.GetComponent<Camera>().fieldOfView = _originalFieldOfView;
            this.GetComponent<Camera>().enabled = false;
        }
    }

}
