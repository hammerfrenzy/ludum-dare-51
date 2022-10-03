using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            PlayGame();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInstructions();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ReturnToMain();
        }
    }

    public void PlayGame()
    {
        audioManager.Stop("Instruction VO");
        SceneManager.LoadScene("SampleScene");
    }

    public void ShowInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void ReturnToMain()
    {
        audioManager.Stop("Instruction VO");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}