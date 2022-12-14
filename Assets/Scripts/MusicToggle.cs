using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicToggle : MonoBehaviour
{
    public AudioManager audioManager;
    private Image image;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        image = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        if(audioManager.muteBgm)
        {
            image.color = Color.clear;
        }
        else
        {
            image.color = Color.white;
        }

    }

    public void ToggleBGM()
    {
        audioManager.ToggleBGM();
    }
}
