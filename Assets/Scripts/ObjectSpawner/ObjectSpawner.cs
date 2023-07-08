using UnityEngine;
using UnscriptedLogic.MathUtils;
using UnscriptedLogic.WaveSystems.Sequential.PointBased;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SpawnerSettings spawnerSettings;
    [SerializeField] private Transform[] spawnLocations;

    private PointBasedWaveSystem waveSystem;
    private int previousChosenIndex;
    private float currentXMovement;

    private void Start()
    {
        waveSystem = new PointBasedWaveSystem(spawnerSettings, OnSpawn, OnCompleted);
        waveSystem.BeginSpawner();
        waveSystem.debugSpawner = true;
        previousChosenIndex = 1;
    }

    private void OnSpawn(GameObject objectToSpawn)
    {
        //For now, we'll set this as only adjacent. We can change this to make gaps in the generator for the actual player to put platforms in between.
        int spawnLocationIndex = SelectRandomAdjacentLocation();

        GameObject spawnable = Instantiate(objectToSpawn);
        spawnable.transform.position = spawnLocations[spawnLocationIndex].transform.position;
    }

    private void Update()
    {
        if (HasTraversedThisRegion()) return;
        currentXMovement = transform.position.x;

        waveSystem.UpdateSpawner();
    }

    private int SelectRandomAdjacentLocation()
    {
        int offset = RandomLogic.BetInts(-1, 2);
        int spawnLocationIndex = previousChosenIndex + offset;
        spawnLocationIndex = Mathf.Clamp(spawnLocationIndex, 0, spawnLocations.Length - 1);

        previousChosenIndex = spawnLocationIndex;
        return spawnLocationIndex;
    }

    //A simple way to make sure we don't spawn platforms in the area we've already spawned them in.
    private bool HasTraversedThisRegion() => transform.position.x < currentXMovement;

    //Since we're not planning on our spawner to end, this function is not needed
    private void OnCompleted(){}
}
