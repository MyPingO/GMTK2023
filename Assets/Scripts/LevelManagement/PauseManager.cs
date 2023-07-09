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
    [System.Serializable]
    public class SpriteLayer
    {
        [SerializeField] private Transform spriteGroup;
        [SerializeField] private float offsetDistance;

        public Transform SpriteGroup => spriteGroup;
        public float OffsetDistance => offsetDistance;
    }

    [SerializeField] private Page pausePage;
    [SerializeField] private PageController pageController;
    [SerializeField] private CinemachineVirtualCamera pauseCamera;

    [Header("Offset")]
    [SerializeField] private float offsetDelay = 0.1f;
    [SerializeField] private float offsetLerpTime = 0.5f;
    [SerializeField] private SpriteLayer[] spriteLayers;
    [SerializeField] private Ease easeType = Ease.InOutQuint;

    private bool isPaused;
    private DepthOfField dofComponent;

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
            StartCoroutine(LerpOutLayers());
        }
        else
        {
            level.Settings.Events.OnResumed?.Invoke();
            pageController.CloseTopPage();
            Time.timeScale = 1;
            LerpInLayers();
        }
    }

    private IEnumerator LerpOutLayers()
    {
        int counter = 0;
        while (counter < spriteLayers.Length)
        {
            yield return new WaitForSecondsRealtime(offsetDelay);
            spriteLayers[counter].SpriteGroup.DOMoveZ(spriteLayers[counter].OffsetDistance, offsetLerpTime).SetEase(easeType);
            counter++;
        }
    }

    private void LerpInLayers()
    {
        for (int i = 0; i < spriteLayers.Length; i++)
        {
            spriteLayers[i].SpriteGroup.DOMoveZ(0, offsetLerpTime).SetEase(Ease.InOutSine);
        }
    }
}
