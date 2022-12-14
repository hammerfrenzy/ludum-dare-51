using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private CanvasGroup timerGroup;
    private Image countdownFiller;
    private float fullBarWidth = 735f;

    void Start()
    {
        timerGroup = GetComponentInParent<CanvasGroup>();
        countdownFiller = GetComponent<Image>();
    }

    public void SetHidden(bool isHidden)
    {
        timerGroup.alpha = isHidden ? 0 : 1;
    }

    public void UpdateWithRemainingTime(float time)
    {
        var percentTimeRemaining = time / 10;
        var currentBarWidth = fullBarWidth * percentTimeRemaining;
        var barHeight = countdownFiller.rectTransform.sizeDelta.y;
        countdownFiller.rectTransform.sizeDelta = new Vector2(currentBarWidth, barHeight);
    }
}
