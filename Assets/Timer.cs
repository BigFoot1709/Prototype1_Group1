using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    private float _time;

    private float _timer;
    private bool _startTimer;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_startTimer)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
               // _timer = _time;
                _startTimer = false;
                this.transform.parent.GetComponent<QuickTimeManager>().CallFailure();
            }
            this.gameObject.GetComponent<Image>().fillAmount = _timer / _time;
        }
    }

    public void StartTimer(float newTime)
    {
         _startTimer = true;
        _timer = newTime;
        _time = newTime;
    }

    public void ResetTimer()
    {
        _startTimer = false;
    }
}
