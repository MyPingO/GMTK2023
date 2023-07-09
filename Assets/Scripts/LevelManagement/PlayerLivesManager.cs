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

        healthHandler = new CurrencyHandler(startLivesAmount, max: 0);
        healthCounterTMP.text = healthHandler.Current.ToString();
    }

    private void OnEnable()
    {
        level.Settings.Events.OnPlayerHit += OnPlayerHitHazard;
    }

    private void OnDisable()
    {
        level.Settings.Events.OnPlayerHit -= OnPlayerHitHazard;
    }

    private void OnPlayerHitHazard(Transform player)
    {
        impulseSource.GenerateImpulse();

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
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        yield return new WaitForSeconds(delay);
        
        player.transform.position = respawnLocation.transform.position;
        rb.simulated = false;

        yield return new WaitForSeconds(1);
        
        rb.simulated = true;

    }
}
