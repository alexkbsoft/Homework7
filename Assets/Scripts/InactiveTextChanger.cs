using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InactiveTextChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _internalText;
    private Button _btn;
    void Start()
    {
        _btn = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {       
        if (_btn.interactable) {
            _internalText.color = Color.white;
        } else {
            _internalText.color = Color.gray;
        }
    }
}
