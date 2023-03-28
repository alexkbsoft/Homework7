using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{
    [SerializeField] private float MaxSeconds = 1.0f;
    [SerializeField] private bool ManualStart = false;
    [SerializeField] private Image _image;
    private float CurrentSeconds;
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

        CurrentSeconds -= Time.deltaTime;

        if (CurrentSeconds <= 0)
        {
            CurrentSeconds = MaxSeconds;

            if (_callback != null)
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

    public void AddCycleCallback(Action doneAction) {
        _callback = doneAction;
    }
}
