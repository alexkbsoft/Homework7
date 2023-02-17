using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{
    public float MaxSeconds = 1.0f;
    public float CurrentSeconds;
    public bool Tick = false;
    private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        CurrentSeconds = MaxSeconds;
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Tick = false;
        CurrentSeconds -= Time.deltaTime;

        if (CurrentSeconds <= 0)
        {
            Tick = true;
            CurrentSeconds = MaxSeconds;
        }

        _image.fillAmount = CurrentSeconds / MaxSeconds;
    }
}
