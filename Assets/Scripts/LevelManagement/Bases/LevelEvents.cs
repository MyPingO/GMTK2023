using System;
using UnityEngine;

//These are the events that are used in the level
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
