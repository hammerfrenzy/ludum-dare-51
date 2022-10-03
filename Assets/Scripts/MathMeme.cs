using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathMeme : MonoBehaviour
{
    public AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void ToggleBGM()
    {
        audioManager.PlayGonGit();
    }
}
