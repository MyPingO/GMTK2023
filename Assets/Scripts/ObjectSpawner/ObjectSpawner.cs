using UnityEngine;
using UnscriptedLogic.MathUtils;
using UnscriptedLogic.WaveSystems.Sequential.PointBased;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SpawnerSettings spawnerSettings;
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private float spawnThreshold = 10f; // The distance to the end of the platforms at which the system will start spawning new ones

    private PointBasedWaveSystem waveSystem;
    private int previousChosenIndex;
    private float endOfPlatformsX;

    private void Start()
    {
        waveSystem = new PointBasedWaveSystem(spawnerSettings, OnSpawn, OnCompleted);
        waveSystem.BeginSpawner();
        waveSystem.debugSpawner = true;
        previousChosenIndex = 1;
        endOfPlatformsX = transform.position.x;
    }

    private void OnSpawn(GameObject objectToSpawn)
    {
        int spawnLocationIndex = SelectRandomAdjacentLocation();
        GameObject spawnable = Instantiate(objectToSpawn);
        spawnable.transform.position = spawnLocations[spawnLocationIndex].transform.position;

        // update the end of the platforms
        endOfPlatformsX = Mathf.Max(endOfPlatformsX, spawnable.transform.position.x);
    }

    private void Update()
    {
        if (!Player.isAlive) return;

        // if the player is close enough to the end of the platforms, spawn new ones
        if (Player.instance.transform.position.x > endOfPlatformsX - spawnThreshold)
        {
            waveSystem.UpdateSpawner();
        }
    }

    private int SelectRandomAdjacentLocation()
    {
        int offset = RandomLogic.BetInts(-1, 2);
        int spawnLocationIndex = previousChosenIndex + offset;
        spawnLocationIndex = Mathf.Clamp(spawnLocationIndex, 0, spawnLocations.Length - 1);

        previousChosenIndex = spawnLocationIndex;
        return spawnLocationIndex;
    }

    private void OnCompleted() {}
}