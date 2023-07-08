using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public UnityEvent onItemPickup;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    List<Item> itemList;

    void Start()
    {
        itemList = new();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Inventory contains " + itemList.Count + " items.");
            
            foreach (Item item in itemList)
            {
                Debug.Log(item.itemName);
            }
        }
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }
    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
    }
    public void SetItemList(List<Item> itemList)
    {
        this.itemList = itemList;
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }


    // function for picking up item on the ground and adding to inventory
    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only).
    private void OnTriggerEnter2D(Collider2D collidedObject)
    {
        Item item = collidedObject.GetComponent<Item>();
        if (item != null)  // If the collided object is an item...
        {
            AddItem(item);  // Add the item to the inventory...
            Destroy(collidedObject.gameObject);  // And remove the item from the world.
            onItemPickup.Invoke();
        }
    }
}
