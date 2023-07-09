using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocksHazard : Hazard
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }

        base.OnTriggerEnter2D(collision);
    }
}
