using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSound : MonoBehaviour
{
   private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Ensure the AudioSource is on the same GameObject
    }

    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
