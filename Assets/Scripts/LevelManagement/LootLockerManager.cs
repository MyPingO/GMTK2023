using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the manager for the accounts and leaderboards
public class LootLockerManager : LevelComponent
{
    private void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("Error starting the lootlocker session");
                return;
            }

            Debug.Log("Successfully established connection to LootLocker");
        });
    }
}
