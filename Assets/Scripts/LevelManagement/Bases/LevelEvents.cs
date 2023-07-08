using System;
using UnityEngine;

public struct LevelEvents
{
    public Action OnGameWon;
    public Action OnGameLost;

    public Action OnEscapePressed;
    public Action OnEscapeReleased;

    public Action OnPaused;
    public Action OnResumed;

    public Action<Transform> OnPlayerHitBoundary;
}
