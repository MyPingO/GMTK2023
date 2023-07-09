using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    public static bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    void OnEnable()
    {
        isAlive = true;
    }
    
    void OnDisable()
    {
        isAlive = false;
    }

}
