using UnityEngine;
using UnityEngine.UI;

// this could probably be a scriptable object, but I don't know how those work
public class Item : MonoBehaviour
{
    static int nextItemID = 0; // The ID of the next item to be created
    public int itemID; // The item's unique ID
    public string itemName; // The item's name
    [SerializeField] Image icon; // The item's icon

    void Start()
    {
        itemID = GetNextItemID();
        icon = GetComponent<Image>();
        if (itemName == null)
        {
            itemName = gameObject.name;
        }
        if (icon == null)
        {
            Debug.LogWarning("Item " + itemName + " does not have an icon!");
        }
    }

    // Set the item's ID to the next available ID and increment the next available ID
    int GetNextItemID()
    {
        int id = nextItemID;
        nextItemID++;
        return id;
    }
}