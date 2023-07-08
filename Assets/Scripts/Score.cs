using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Score found!");
            return;
        }
        instance = this;
    }
    public TMP_Text distanceText;
    public TMP_Text diamondsText;
    public float distance = 0;  //referenced in PlayerMovement.cs
    public int diamonds = 0;
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
