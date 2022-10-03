using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SFXToggle : MonoBehaviour
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
        if (audioManager.muteSfx)
        {
            image.color = Color.clear;
        }
        else
        {
            image.color = Color.white;
        }

    }

    public void ToggleSFX()
    {
        audioManager.ToggleSFX();
    }
}
