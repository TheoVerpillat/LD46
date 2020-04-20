using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private AudioSource buttonSound;
    private bool esc = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            esc = true;
            Pause();
        }
    }

    public void Pause()
    {
        if (!esc) {
            if (buttonSound) { buttonSound.Play(); }
        }

        if (isPaused)
        {
            pauseCanvas.SetActive(false);
            isPaused = false;
        }
        else
        {
            pauseCanvas.SetActive(true);
            isPaused = true;
        }

        esc = false;
    }
}

