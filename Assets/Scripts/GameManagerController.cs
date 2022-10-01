using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    // Assigned in Editor
    // Scales up to cover the screen
    // so that Snek can have private time
    public GameObject GameResetOverlay;

    // Prefab mate for spawning
    public GameObject MatePrefab;

    private SnekController snek;
    private List<GameObject> mates;
    private LerpCamera lerpCamera;
    private TimerUI timerUI;
    private float timer = 10f;
    private bool isResetting = false;

    void Start()
    {
        snek = FindObjectOfType<SnekController>();
        lerpCamera = FindObjectOfType<LerpCamera>();
        timerUI = FindObjectOfType<TimerUI>();
        mates = new List<GameObject>();

        SpawnMates();
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
        // Also allows us to do "mid mate" work.

        GameResetOverlay
            .transform
            .DOScale(new Vector3(100, 100, 1), 1f)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                snek.Reset();
                var snekPosition = snek.transform.position;

                // Move camera to snek quickly
                lerpCamera.SnapToPosition(snekPosition);

                // Move overlay so that it shrinks to new snek position
                GameResetOverlay.transform.position = snek.transform.position;

                ShrinkOverlay();
            });
    }

    private void ShrinkOverlay()
    {
        GameResetOverlay
        .transform
        .DOScale(new Vector3(0, 0, 1), 1f)
        .SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
            snek.EndMating();
            timer = 10f;
        });
    }

    private void SpawnMates()
    {
        RemoveMates();

        var spawnRingCount = 4;
        var ringSpacing = 10;
        var startMates = 2;

        for (int i = 0; i < spawnRingCount; i++)
        {
            var matesInThisRing = startMates + (i * 2);
            var radialSpacing = (360f / matesInThisRing) * Mathf.Deg2Rad;

            for (int m = 0; m < matesInThisRing; m++)
            {
                // - Choose point on edge of ring
                var ringDistance = (i + 1) * ringSpacing;

                // - Offset rotation by cos/sin(360/m)
                var dr = radialSpacing * m; // should have random offset (maybe +/- ~0.2 radians?)
                var dx = Mathf.Cos(dr) * ringDistance;
                var dy = Mathf.Sin(dr) * ringDistance;

                var radialOffset = new Vector2(dx, dy);


                // - Offset within unit circle
                var flatOffset = Random.insideUnitCircle * 0.25f;

                var finalPosition = radialOffset;// + offset;
                var mateObject = Instantiate(MatePrefab, finalPosition, Quaternion.identity);

                // - Assign Traits
                var mate = mateObject.GetComponent<MateController>();
                mate.GetComponentsDuringSpawn();
                mate.AddTraitsPreferring(snek);
            }
        }
    }

    private void RemoveMates()
    {
        foreach (var mate in mates)
        {
            Destroy(mate);
        }

        mates.Clear();
    }
}
