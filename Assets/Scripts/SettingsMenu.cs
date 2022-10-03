using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public bool visible = false;
    private RectTransform rectTransform;
    private Tween displayTween;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (visible)
        {
            displayTween?.Kill();
            displayTween = rectTransform.DOAnchorPosX(338, 0.5f);
        }
        else
        {
            displayTween?.Kill();
            displayTween = rectTransform.DOAnchorPosX(1366, 0.5f);
        }
    }

    public void ToggleVisibility()
    {
        visible = !visible;
    }
}
