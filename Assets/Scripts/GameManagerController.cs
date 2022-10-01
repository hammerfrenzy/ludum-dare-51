using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    // Assigned in Editor
    // Scales up to cover the screen
    // so that Snek can have private time
    public GameObject GameResetOverlay;

    public float timer = 10f;
    SnekController snek;

    private TimerUI timerUI;
    private bool isResetting = false;

    void Start()
    {
        snek = GameObject.Find("Snek").GetComponent<SnekController>();
        timerUI = FindObjectOfType<TimerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isResetting) return;

        timer -= Time.deltaTime;
        timerUI.UpdateWithRemainingTime(timer);
        if (timer < 0)
        {
            // Game Over screen here
            UnityEngine.Debug.Log("Your bloodline is dead");
        }
    }

    // Player found a mate
    // Reset the board state
    public void MateReset()
    {
        // Play Mate animation here
        var screenSize = new Vector2(Screen.width, Screen.height);
        var heartPosition = Camera.main.ScreenToWorldPoint(screenSize / 2);
        GameResetOverlay.transform.position = new Vector3(heartPosition.x, heartPosition.y, 0);

        snek.StartMating();

        // Scale up & back down.
        // When finished, start the timer & give control back to player.
        // Two separate tweens because the Yoyo loop wasn't returning back to zero.

        GameResetOverlay
            .transform
            .DOScale(new Vector3(100, 100, 1), 1f)
            .OnComplete(() =>
            {
                snek.Reset();
                ShrinkOverlay();
            });
    }

    private void ShrinkOverlay()
    {
        GameResetOverlay
        .transform
        .DOScale(new Vector3(0, 0, 1), 1f)
        .OnComplete(() =>
        {
            snek.EndMating();
            timer = 10f;
        });
    }
}
