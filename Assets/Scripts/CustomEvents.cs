using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomEvents : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip ClickClip;
    private Button _btn;

    void Start()
    {
        _btn = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(BaseEventData eventData)
    {
        if (_btn.interactable) {
            AudioSource.clip = ClickClip;
            AudioSource.Play();
        }
    }
}
