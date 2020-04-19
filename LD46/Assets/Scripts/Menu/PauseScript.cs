using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject pauseCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
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
    }
}

