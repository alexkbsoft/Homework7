using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsPlayer : MonoBehaviour
{
    public AudioClip eat;
    public AudioClip spawn;
    public AudioClip fight;
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Eat() {
        _audioSource.clip = eat;
        _audioSource.Play();
    }

    public void Spawn() {
        _audioSource.clip = spawn;
        _audioSource.Play();
    }
    public void Fight() {
        _audioSource.clip = fight;
        _audioSource.Play();
    }
}
