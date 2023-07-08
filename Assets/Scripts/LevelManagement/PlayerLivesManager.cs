using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnscriptedLogic.Currency;
using UnscriptedLogic.MathUtils;

public class PlayerLivesManager : LevelComponent
{
    [Header("Initial Values")]
    [SerializeField] private int startLivesAmount = 3;

    [Header("Respawning")]
    [SerializeField] private Transform respawnLocation;
    [SerializeField] private float respawnDelay;

    private CurrencyHandler healthHandler;

    protected override void Awake()
    {
        base.Awake();

        healthHandler = new CurrencyHandler(startLivesAmount, max: 9);
    }

    private void OnEnable()
    {
        level.Settings.Events.OnPlayerHitBoundary += OnPlayerHitBoundary;
    }

    private void OnDisable()
    {
        level.Settings.Events.OnPlayerHitBoundary -= OnPlayerHitBoundary;
    }

    private void OnPlayerHitBoundary(Transform player)
    {
        player.gameObject.SetActive(false);

        healthHandler.Modify(ModifyType.Subtract, 1);

        if (healthHandler.Current <= 0)
        {
            level.Settings.Events.OnGameLost?.Invoke();
            return;
        }

        StartCoroutine(RespawnPlayerWithDelay(player, respawnDelay));
    }

    private IEnumerator RespawnPlayerWithDelay(Transform player, float delay)
    {
        yield return new WaitForSeconds(delay);

        player.transform.position = respawnLocation.transform.position;

        player.gameObject.SetActive(true);
    }
}
