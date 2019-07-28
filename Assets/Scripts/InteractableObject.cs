using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    private bool _clickedOnce;
    private bool _doubleClicked;
    private float _doubleClickTimer;
    private float _doubleClickDelay;
    private bool _startTimer;

    public float zoomInLimit;
    public float zoomOutLimit;
    public float startingZoomScale;
    public Vector3 myCameraRot;
    public bool lockRotation;
    public bool returnZoom;
    public Sprite hintSprite;
    
    private bool _currActiveObject;
    private static List<GameObject> _interactableObjects;
    private GameObject _focusCamera;

    public UnityEvent m_OnZoomFinished;

    private void Awake()
    {
        _interactableObjects = new List<GameObject>();
        _focusCamera = GameObject.Find("FocusCamera");
        _focusCamera.GetComponent<Camera>().enabled = false;
    }
    // Use this for initialization
    void Start()
    {
        _clickedOnce = false;
        _doubleClicked = false;
        _doubleClickTimer = 0.0f;
        _doubleClickDelay = 0.5f;
        _startTimer = false;

        _interactableObjects.Add(this.gameObject);

        foreach (GameObject g in _interactableObjects)
        {
            print("Object: " + g.name + "\n");
        }

        if (m_OnZoomFinished == null)
        {
            m_OnZoomFinished = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {

        //if (_currActiveObject == false)
        //{
        //    this.GetComponent<BoxCollider>().enabled = true;
        //}

        print(_doubleClicked);

        if (_startTimer)
        {
            _doubleClickTimer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_clickedOnce == false)
            {
                _clickedOnce = true;
                _startTimer = true;
            }
            else if (_doubleClickTimer <= _doubleClickDelay)
            {
                _doubleClicked = true;
            }
        }

        if (_doubleClickTimer > _doubleClickDelay)
        {
            _clickedOnce = false;
            _doubleClicked = false;
            _startTimer = false;
            _doubleClickTimer = 0.0f;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetAxis("PS4_R2")>0)
        {
            print(gameObject.name);        
            _focusCamera.GetComponent<Camera>().enabled = true;
            //GameObject.Find("FreeLookCameraRig").SetActive(false);
            _focusCamera.GetComponent<FocusCamera>().ChangeFinishedZoomEvent(m_OnZoomFinished);
            _focusCamera.GetComponent<FocusCamera>().FocusOnObject(this.gameObject.transform);
            ClearClicks();
        }
    }

    void OnMouseExit()
    {
        print("OffObject");
        ClearClicks();
    }

    void ClearClicks()
    {
        _doubleClicked = false;
        _clickedOnce = false;
        _startTimer = false;
        _doubleClickTimer = 0.0f;
    }

    void MakeThisCameraTarget()
    {
        AudioManager.PlaySound(Resources.Load("CameraWooshSwitch") as AudioClip);
        GameObject.Find("FocusCamera").GetComponent<FocusCamera>().FocusOnObject(this.gameObject.transform);
        _currActiveObject = true;
        this.GetComponent<BoxCollider>().enabled = false;
        if (lockRotation)
        {
            GameObject.Find("RotateCamera").transform.rotation = Quaternion.Euler(myCameraRot);
        }

        foreach (GameObject g in _interactableObjects)
        {
            g.GetComponent<BoxCollider>().enabled = false;
        }

    }

    public float GetZoomInLimit()
    {
        return zoomInLimit;
    }

    public float GetZoomOutLimit()
    {
        return zoomOutLimit;
    }

    public float GetStartingZoomScale()
    {
        return startingZoomScale;
    }

    public void SetCurrActiveObj(bool active)
    {
        _currActiveObject = active;
    }

    public bool IsCurrActiveObj()
    {
        return _currActiveObject;
    }

    public static List<GameObject> GetAllInteractableObjects()
    {
        return _interactableObjects;
    }

    public void MakeThisCameraTargetExternally()
    {
        bool switchTargets = true;

        GameObject[] rememberWords = GameObject.FindGameObjectsWithTag("Remember");

        foreach (GameObject g in rememberWords)
        {
            if (g.GetComponent<SpriteRenderer>().color.a < 0.5f)
            {
                switchTargets = false;
            }
        }
        if (switchTargets)
        {
            MakeThisCameraTarget();
        }

        GameObject.Find("AudioManager").GetComponents<AudioSource>()[0].volume = 0;
        GameObject.Find("AudioManager").GetComponents<AudioSource>()[1].volume = 0;
    }

    public void ChangeOnZoomFinishedEvent(UnityEvent zoom)
    {
        m_OnZoomFinished = zoom;
    }

    public static void EnableAllInteractableObjects()
    {
        foreach (GameObject i in _interactableObjects)
        {
            i.GetComponent<BoxCollider>().enabled = true;
            Debug.Log("I HAVE ENABLED ALL INTERACTABLE OBJECTS");
        }
    }

    public static void DisableAllInteractableObjects()
    {
        foreach (GameObject i in _interactableObjects)
        {
            i.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
