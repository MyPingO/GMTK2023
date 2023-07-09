using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is what derives as a component that interacts at the level of a LevelManager.
//Typically these are scripts that require listening and invoking of level events
public class LevelComponent : MonoBehaviour
{
    protected LevelController level;

    protected virtual void Awake()
    {
        level = LevelController.instance;
    }
}
