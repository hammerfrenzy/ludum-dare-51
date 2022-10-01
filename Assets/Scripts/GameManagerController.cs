using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public float timer = 10f;
    SnekController snek;

    private TimerUI timerUI;

    void Start()
    {
        snek = GameObject.Find("Snek").GetComponent<SnekController>();
        timerUI = FindObjectOfType<TimerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerUI.UpdateWithRemainingTime(timer);
        if (timer < 0)
        {
            // Game Over screen here
            UnityEngine.Debug.Log("Your bloodline is dead");
        }
    }

    public void MateReset()
    {
        // Player found a mate
        // Play Mate animation here
        timer = 10f;
        snek.ResetSnek();
    }
}
