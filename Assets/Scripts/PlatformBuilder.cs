using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlatformBuilder : MonoBehaviour
{
    public static PlatformBuilder instance;
    public float rotationSpeed = 10f;  // adjust this to change how fast the platform rotates

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlatformBuilder found!");
            return;
        }
        instance = this;
    }

    GameObject platformToBuildPrefab;
    GameObject platformToBuildInstance;
    [SerializeField] GameObject[] platformPrefabs;

    public GameObject GetPlatformToBuild()
    {
        return platformToBuildPrefab;
    }

    public void SelectPlatform(GameObject platformPrefab)
    {
        if (platformToBuildInstance != null)
            Destroy(platformToBuildInstance);

        platformToBuildPrefab = platformPrefab;

        platformToBuildInstance = Instantiate(platformPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        platformToBuildInstance.GetComponentInChildren<Collider2D>().enabled = false;
        platformToBuildInstance.GetComponentInChildren<ShadowCaster2D>().enabled = false;

        // Change the opacity of the platform
        SpriteRenderer spriteRenderer = platformToBuildInstance.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;
        }

        // Disable the Collider component to prevent interactions
        Collider2D collider = platformToBuildInstance.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    public void BuildPlatform(Vector3 position)
    {
        // Instantiate the platform at the position and enable its Collider
        GameObject platformInstance = Instantiate(platformToBuildPrefab, position, platformToBuildInstance.transform.rotation);

        Collider2D collider = platformInstance.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        // Clear the platformToBuildInstance since it's now been built
        if (platformToBuildInstance != null)
        {
            Destroy(platformToBuildInstance);
            platformToBuildInstance = null;
        }
    }

    void Update()
    {

        // check if player presses 1, 2, 3 or 4 to select a platform

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectPlatform(platformPrefabs[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectPlatform(platformPrefabs[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectPlatform(platformPrefabs[2]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectPlatform(platformPrefabs[3]);
        }

        if (platformToBuildInstance != null)
        {
            // Convert mouse position to world point
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            platformToBuildInstance.transform.position = mousePosition;

            // Rotate the platform based on mouse wheel
            float rotation = Input.GetAxis("Mouse ScrollWheel") * rotationSpeed;
            platformToBuildInstance.transform.Rotate(0f, 0f, -rotation);

            // Check for mouse click to build the platform
            if (Input.GetMouseButtonDown(0))
            {
                // Check if the mouse is over a UI element
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    BuildPlatform(mousePosition);
                }
            }
        }
    }
}
