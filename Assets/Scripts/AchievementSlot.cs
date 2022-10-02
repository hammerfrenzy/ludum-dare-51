using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    private Image image;
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {
        canvasGroup.alpha = 1.0f;
    }
}
