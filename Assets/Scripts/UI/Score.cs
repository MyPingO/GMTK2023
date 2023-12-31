using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : LevelComponent
{
    public static Score instance;

    public TMP_Text distanceText;
    public TMP_Text diamondsText;
    public float distance = 0;  //referenced in PlayerController.cs
    public int diamonds = 0;

    protected override void Awake()
    {
        base.Awake();

        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Score found!");
            return;
        }
        instance = this;
    }

    void Start()
    {
        Inventory.instance.onItemPickup.AddListener(UpdateDiamondCount);
    }
    void Update()
    {
        distanceText.text = "Distance: " + distance.ToString("F0");
    }

    void UpdateDiamondCount()
    {
        diamonds++;
        diamondsText.text = diamonds.ToString();
    }
}
