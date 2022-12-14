using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private WinScreenUI winScreenUI;
    private float timer = 10f;
    private bool isResetting = false;
    private bool gameOver = false;

    void Start()
    {
        snek = FindObjectOfType<SnekController>();
        lerpCamera = FindObjectOfType<LerpCamera>();
        timerUI = FindObjectOfType<TimerUI>();
        winScreenUI = FindObjectOfType<WinScreenUI>();
        mates = new List<GameObject>();

        SpawnMates();
    }

    // Update is called once per frame
    void Update()
    {
        if (isResetting || gameOver) return;

        timer -= Time.deltaTime;
        timerUI.UpdateWithRemainingTime(timer);
        if (timer < 0)
        {
            gameOver = true;
            snek.KillSnek();
            //gameOverUI.setGameOverVisible(true);
        }
    }

    // Player found a mate
    // Reset the board state
    public void MateReset()
    {
        isResetting = true;

        var screenSize = new Vector2(Screen.width, Screen.height);
        var heartPosition = Camera.main.ScreenToWorldPoint(screenSize / 2);
        GameResetOverlay.transform.position = new Vector3(heartPosition.x, heartPosition.y, 0);
        timerUI.SetHidden(true);

        snek.DisableMovement();

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
                RemoveMates();
                SpawnMates();
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
            if (snek.CheckWinCondition())
            {
                gameOver = true;
                winScreenUI.setWinScreenVisible(true, snek.GetPrimaryPhenotype());

                lerpCamera.disableCameraMovement = true;
                snek.DisableMovement();
                snek.SendToWinScreenBox();
            }
            else
            {
                timer = 10f;
                timerUI.SetHidden(false);
                isResetting = false;
            }
        });
    }

    private void SpawnMates()
    {
        var totalSpawnRings = 7;
        var ringSpacing = 7;
        var startMates = 1;
        var radialRandomness = Mathf.PI / 8;

        for (int i = 0; i < totalSpawnRings; i++)
        {
            var matesInThisRing = startMates + (i * 2);
            var radialSpacing = (360f / matesInThisRing) * Mathf.Deg2Rad;
            var ringDistance = (i + 1) * ringSpacing;
            var radialOffsetForRing = Random.Range(-Mathf.PI / 2, Mathf.PI / 2);

            for (int m = 0; m < matesInThisRing; m++)
            {
                // Randomize radial position
                var radialOffsetForMate = Random.Range(-radialRandomness, radialRandomness);
                var dr = (radialSpacing * m) + radialOffsetForRing;
                var dx = Mathf.Cos(dr) * ringDistance;
                var dy = Mathf.Sin(dr) * ringDistance;

                var finalRadialOffset = new Vector2(dx, dy);

                // - Offset within unit circle
                var flatOffset = Random.insideUnitCircle * 0.15f;

                var finalPosition = finalRadialOffset + flatOffset;
                var mateObject = Instantiate(
                    MatePrefab,
                    finalPosition,
                    Quaternion.identity,
                    transform);

                mates.Add(mateObject);

                // - Assign Traits
                var mate = mateObject.GetComponent<MateController>();
                mate.GetComponentsDuringSpawn();
                mate.AddTraitsPreferring(snek, i);
            }
        }
    }

    private void RemoveMates()
    {
        foreach (var mate in mates)
        {
            Destroy(mate.gameObject);
        }

        mates.Clear();
    }
}
