using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public GameObject uiCanvas;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void setGameOverVisible(bool visible)
    {
        if (visible)
        {
            Canvas canvas = uiCanvas.GetComponent<Canvas>();
            canvas.sortingLayerName = "UI Over Player";
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
        }
    }

}
