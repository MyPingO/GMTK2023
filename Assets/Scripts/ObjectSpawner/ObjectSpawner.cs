using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnscriptedLogic.WaveSystems.Sequential.PointBased;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private SpawnerSettings spawnerSettings;
    private PointBasedWaveSystem waveSystem;

    private void Start()
    {
        waveSystem = new PointBasedWaveSystem(spawnerSettings, OnSpawn, OnCompleted);
    }

    private void OnSpawn(GameObject objectToSpawn)
    {

    }

    //Since we're not planning on our spawner to end, this function is not needed
    private void OnCompleted(){}

    private void Update()
    {
        waveSystem.UpdateSpawner();
    }
}
