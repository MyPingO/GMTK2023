using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : LevelComponent
{
    [SerializeField] private Page pausePage;
    [SerializeField] private PageController pageController;
    [SerializeField] private CinemachineVirtualCamera pauseCamera;

    private bool isPaused;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        level.Settings.Events.OnEscapePressed += PauseGame;
    }

    private void OnDisable()
    {
        level.Settings.Events.OnEscapePressed -= PauseGame;
    }

    private void PauseGame()
    {
        isPaused = !isPaused;
        pauseCamera.Priority = isPaused ? 0 : -1;

        if (isPaused)
        {
            level.Settings.Events.OnPaused?.Invoke();
            pageController.OpenPage(pausePage);
            Time.timeScale = 0;
        }
        else
        {
            level.Settings.Events.OnResumed?.Invoke();
            pageController.CloseTopPage();
            Time.timeScale = 1;
        }
    }
}
