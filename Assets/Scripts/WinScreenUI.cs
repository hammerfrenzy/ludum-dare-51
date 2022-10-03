using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Image image;
    private AudioManager audioManager;
    public Sprite trogdorBlue;
    public Sprite cyborgPurple;
    public Sprite seaGreen;
    public Sprite miscOrange;

    void Start()
    {
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (!canvasGroup.interactable) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReturnToMenu();
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void setWinScreenVisible(bool visible, Phenotype snekPhenotype)
    {
        switch (snekPhenotype)
        {
            case Phenotype.Blue:
                image.sprite = trogdorBlue;
                audioManager.Play("blue ending");
                break;
            case Phenotype.Purple:
                image.sprite = cyborgPurple;
                audioManager.Play("purple ending");
                break;
            case Phenotype.Green:
                image.sprite = seaGreen;
                audioManager.Play("green ending");
                break;
            case Phenotype.Orange:
                image.sprite = miscOrange;
                audioManager.Play("orange ending");
                break;
        }

        if (visible)
        {
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
