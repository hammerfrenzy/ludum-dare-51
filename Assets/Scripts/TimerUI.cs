using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private Image countdownFiller;
    private float fullBarWidth = 800f;

    void Start()
    {
        countdownFiller = GetComponent<Image>();
    }

    public void UpdateWithRemainingTime(float time)
    {
        var percentTimeRemaining = time / 10;
        var currentBarWidth = fullBarWidth * percentTimeRemaining;
        var barHeight = countdownFiller.rectTransform.sizeDelta.y;
        countdownFiller.rectTransform.sizeDelta = new Vector2(currentBarWidth, barHeight);
    }
}
