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
                audioManager.Play("Trogdor Ending");
                break;
            case Phenotype.Purple:
                image.sprite = cyborgPurple;
                audioManager.Play("Tech Ending");
                break;
            case Phenotype.Green:
                image.sprite = seaGreen;
                audioManager.Play("Sea Ending");
                break;
            case Phenotype.Orange:
                image.sprite = miscOrange;
                audioManager.Play("Misc Ending");
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
