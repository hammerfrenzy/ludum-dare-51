using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Test()
    {
        canvasGroup.alpha = 1.0f;
    }
}
