using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] LevelSettingsSO levelSettings;

    void OnEnable() {
        levelSettings = Resources.Load<LevelSettingsSO>("LevelDetails");
        levelSettings.Events.OnPlayerHit += OnPlayerHit;
    }

    void OnDisable() {
        levelSettings.Events.OnPlayerHit -= OnPlayerHit;
    }

    void OnPlayerHit(Transform player) {
        if (player == transform) {
            Debug.Log("Player hit by hazard");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            levelSettings.Events.OnPlayerHit?.Invoke(collision.transform);
        }
    }
}
