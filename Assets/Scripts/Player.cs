using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Player found!");
            return;
        }
        instance = this;
    }
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
