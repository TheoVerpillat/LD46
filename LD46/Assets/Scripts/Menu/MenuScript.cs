using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject creditsCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject howToCanvas;
    [SerializeField] private AudioSource buttonSound;

    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;
    [SerializeField] private GameObject page3;
    private int currentPage;

    //[SerializeField] private GameObject hero;
    private GameObject backCanvas;
    //private PauseScript pauseScript;

    private void Start()
    {
        //pauseScript = gameObject.GetComponent<PauseScript>();
        //pauseScript.inGame = false;
    }

    public void StartGame()
    {
        if (buttonSound) { buttonSound.Play(); }
        //pauseScript.inGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        if (buttonSound) { buttonSound.Play(); }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartCredits()
    {
        if (buttonSound) { buttonSound.Play(); }

        if (menuCanvas.activeSelf)
        {
            menuCanvas.SetActive(false);
            backCanvas = menuCanvas;
        }
        else
        {
            pauseCanvas.SetActive(false);
            backCanvas = pauseCanvas;
        }

        creditsCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        if (buttonSound) { buttonSound.Play(); }
        Application.Quit();
    }

    public void GoToMenu()
    {
        if (buttonSound) { buttonSound.Play(); }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Back()
    {
        creditsCanvas.SetActive(false);
        backCanvas.SetActive(true);
    }

    public void StartHowTo()
    {
        currentPage = 1;
        howToCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    public void NextPage()
    {
        if (currentPage == 1)
        {
            currentPage = 2;
            page2.SetActive(true);
            page1.SetActive(false);
        } else if (currentPage == 2)
        {
            currentPage = 3;
            page3.SetActive(true);
            page2.SetActive(false);
        } else if (currentPage == 3)
        {
            currentPage = 1;
            page1.SetActive(true);
            menuCanvas.SetActive(true);
            page3.SetActive(false);
            howToCanvas.SetActive(false);
        }
    }

    public void PreviousPage()
    {
        if (currentPage == 1)
        {
            howToCanvas.SetActive(false);
            menuCanvas.SetActive(true);
        } else if (currentPage == 2)
        {
            currentPage = 1;
            page1.SetActive(true);
            page2.SetActive(false);
        } else if (currentPage == 3)
        {
            currentPage = 2;
            page3.SetActive(false);
            page2.SetActive(true);
        }
    }
}
