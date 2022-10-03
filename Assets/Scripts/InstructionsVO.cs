using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsVO : MonoBehaviour
{
    private AudioManager audioManager;
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Instruction VO");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
