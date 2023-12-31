using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIPageController : PageController
{
    [SerializeField] private Page pausePage;
    [SerializeField] private Page gameLostPage;

    private void OnEnable()
    {
        level.Settings.Events.OnPaused += OnPaused;
        level.Settings.Events.OnResumed += OnResumed;
        level.Settings.Events.OnGameLost += OnGameLost;
    }

    private void OnDisable()
    {
        level.Settings.Events.OnPaused -= OnPaused;
        level.Settings.Events.OnResumed -= OnResumed;
        level.Settings.Events.OnGameLost -= OnGameLost;
    }

    private void OnGameLost()
    {
        StartCoroutine(OpenGameLostPage());
    }

    private void OnResumed()
    {
        PopAllTillRoot();
    }

    private void OnPaused()
    {
        OpenPage(pausePage);
    }

    private IEnumerator OpenGameLostPage()
    {
        yield return new WaitForSecondsRealtime(3f);
        OpenPage(gameLostPage);
    }
}
