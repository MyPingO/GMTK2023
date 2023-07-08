using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }
}
