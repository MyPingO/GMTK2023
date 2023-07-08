using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBDetector : MonoBehaviour
{
    [SerializeField] private LevelSettingsSO levelSettings;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            levelSettings.Events.OnPlayerHitBoundary?.Invoke(collision.transform);
        }
    }
}
