using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Level Controllers are the heads of every level component. It's here where generic stuff about the level can be added such as names, descriptions and events of the level
//that would propogate actions to any listeners
[DisallowMultipleComponent]
[DefaultExecutionOrder(-1)]
public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelSettingsSO levelSettings;
    public LevelSettingsSO Settings => levelSettings;

    public static LevelController instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator SceneExitCleanUp_Coroutine()
    {
        throw new NotImplementedException();
    }
}
