using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBDetector : MonoBehaviour
{
    public Transform spawnPoint;

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = spawnPoint.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RespawnPlayer(collision.gameObject);
        }
    }
}
