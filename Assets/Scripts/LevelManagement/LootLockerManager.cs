using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the manager for the accounts and leaderboards
public class LootLockerManager : LevelComponent
{
    private const string LEADERBOARD_FURTHEST_ID = "15937";
    public const string PLAYERPREFS_PLAYERID = "PlayerID";

    private void Start()
    {
        StartCoroutine(GuestLogIn());
    }

    private IEnumerator GuestLogIn()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("Error starting the lootlocker session");
                PlayerPrefs.SetString(PLAYERPREFS_PLAYERID, response.player_id.ToString());
                done = true;
            } else
            {
                Debug.Log("Successfully established connection to LootLocker");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public static IEnumerator SubmitScoreRoutine(int score)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString(PLAYERPREFS_PLAYERID);
        LootLockerSDKManager.SubmitScore(playerID, score, LEADERBOARD_FURTHEST_ID, (res) =>
        {
            if (res.success)
            {
                Debug.Log("Successfully uploaded score");
                done = true;
            } else
            {
                Debug.Log($"Something went wrong: {res.Error}");
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }
}
