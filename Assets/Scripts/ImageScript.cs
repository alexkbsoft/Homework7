using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{
    public float MaxSeconds = 1.0f;
    public float CurrentSeconds;
    public bool Tick = false;
    public bool ManualStart = false;
    private Image _image;
    private Action _callback;
    private bool _isPalying = false;
    // Start is called before the first frame update
    void Start()
    {
        CurrentSeconds = MaxSeconds;
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ManualStart && !_isPalying) return;

        Tick = false;
        CurrentSeconds -= Time.deltaTime;

        if (CurrentSeconds <= 0)
        {
            Tick = true;
            CurrentSeconds = MaxSeconds;

            if (ManualStart && _callback != null)
            {
                _isPalying = false;
                _callback.Invoke();
            }
        }

        _image.fillAmount = CurrentSeconds / MaxSeconds;
    }

    public void Play(float seconds, Action onDone)
    {
        ManualStart = true;
        _isPalying = true;
        _callback = onDone;
        MaxSeconds = seconds;
        CurrentSeconds = MaxSeconds;
    }
}
