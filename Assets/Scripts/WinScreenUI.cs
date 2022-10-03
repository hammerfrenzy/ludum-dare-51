using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Image image;
    public Button backButton;
    public Sprite trogdorBlue;
    public Sprite cyborgPurple;
    public Sprite seaGreen;
    public Sprite miscOrange;
    void Start()
    {
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void setWinScreenVisible(bool visible, Phenotype snekPhenotype)
    {
        switch(snekPhenotype)
        {
            case Phenotype.Blue:
                image.sprite = trogdorBlue;
                break;
            case Phenotype.Purple:
                image.sprite = cyborgPurple;
                break;
            case Phenotype.Green:
                image.sprite = seaGreen;
                break;
            case Phenotype.Orange:
                image.sprite = miscOrange;
                break;
        }

        if (visible)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            backButton.interactable = true;
            backButton.image.enabled = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            backButton.interactable = false;
            backButton.image.enabled = false;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
