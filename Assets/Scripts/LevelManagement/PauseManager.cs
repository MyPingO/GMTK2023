using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PauseManager : LevelComponent
{
    [SerializeField] private Page pausePage;
    [SerializeField] private PageController pageController;
    [SerializeField] private CinemachineVirtualCamera pauseCamera;
    [SerializeField] private VolumeProfile volume;

    private bool isPaused;
    private DepthOfField dofComponent;

    protected override void Awake()
    {
        base.Awake();

        volume.TryGet(out dofComponent);
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
            dofComponent.focusDistance.value = 2;
        }
        else
        {
            level.Settings.Events.OnResumed?.Invoke();
            pageController.CloseTopPage();
            Time.timeScale = 1;
            dofComponent.focusDistance.value = 10;
        }
    }
}
