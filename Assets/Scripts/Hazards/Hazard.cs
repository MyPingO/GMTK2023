using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] LevelSettingsSO levelSettings;

    protected virtual void OnEnable() {
        levelSettings = Resources.Load<LevelSettingsSO>("LevelDetails");
        levelSettings.Events.OnPlayerHit += OnPlayerHit;
    }

    protected virtual void OnDisable() {
        levelSettings.Events.OnPlayerHit -= OnPlayerHit;
    }

    protected virtual void OnPlayerHit(Transform player) {
        if (player == transform) {
            Debug.Log("Player hit by hazard");
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            levelSettings.Events.OnPlayerHit?.Invoke(collision.transform);
        }
    }
}
