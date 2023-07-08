using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI healthCounterTMP;

    [Header("Components")]
    [SerializeField] private CinemachineImpulseSource impulseSource;

    private CurrencyHandler healthHandler;

    protected override void Awake()
    {
        base.Awake();

        healthHandler = new CurrencyHandler(startLivesAmount, max: 9);
        healthCounterTMP.text = healthHandler.Current.ToString();
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
        impulseSource.GenerateImpulse();
        player.gameObject.SetActive(false);

        healthHandler.Modify(ModifyType.Subtract, 1);
        healthCounterTMP.text = healthHandler.Current.ToString();

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

        yield return new WaitForSeconds(delay);

        player.gameObject.SetActive(true);
    }
}
