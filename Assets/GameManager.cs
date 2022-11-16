using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Button pause;
    public Button resume;
    public bool Paused = false;
    public void PauseGame()
    {
        Time.timeScale = 0;
        Paused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Paused = false ;
    }

    private void Start()
    {
        pause.onClick.AddListener(PauseGame);
        resume.onClick.AddListener(ResumeGame);
    }
}
