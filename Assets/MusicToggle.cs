using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToggle : MonoBehaviour
{
    public AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void ToggleBGM()
    {
        audioManager.ToggleBGM();
    }

    public void ToggleSFX()
    {
        audioManager.ToggleSFX();
    }
}
