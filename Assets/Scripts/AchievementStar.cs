using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementStar : MonoBehaviour
{
    // Start is called before the first frame update
    public string starColor;
    public bool starEnabled = false;
    private Image image;
    private RectTransform rectTransform;
    // Update is called once per frame

    private void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }
    void FixedUpdate()
    {
        if(starEnabled)
        {
            rectTransform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rectTransform.localScale = new Vector3(0, 0, 0);
        }
    }
}
