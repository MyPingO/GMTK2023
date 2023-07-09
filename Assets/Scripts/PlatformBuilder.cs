using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class PlatformBuilder : MonoBehaviour
{
    public static PlatformBuilder instance;
    public float rotationSpeed = 10f;
    public float[] platformCooldowns;
    private float[] platformTimestamps;

    public Button[] platformButtons;

    [SerializeField] GameObject[] platformPrefabs;
    GameObject platformToBuildPrefab;
    GameObject platformToBuildInstance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlatformBuilder found!");
            return;
        }
        instance = this;

        platformTimestamps = new float[platformCooldowns.Length];
    }

    public GameObject GetPlatformToBuild()
    {
        return platformToBuildPrefab;
    }

    public void SelectPlatform(int platformIndex)
    {
        if (platformToBuildInstance != null)
            Destroy(platformToBuildInstance);

        platformToBuildPrefab = platformPrefabs[platformIndex];

        platformToBuildInstance = Instantiate(platformPrefabs[platformIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        platformToBuildInstance.GetComponentsInChildren<Collider2D>()[0].enabled = false;
        platformToBuildInstance.GetComponentsInChildren<Collider2D>()[1].enabled = false;
        platformToBuildInstance.GetComponentInChildren<ShadowCaster2D>().enabled = false;

        // Change the opacity of the platform
        SpriteRenderer spriteRenderer = platformToBuildInstance.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0.25f;
            spriteRenderer.color = color;
        }

        // Disable the Collider component to prevent interactions
        Collider2D collider = platformToBuildInstance.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    public void BuildPlatform(Vector3 position, int platformIndex)
    {
        GameObject platformInstance = Instantiate(platformToBuildPrefab, position, platformToBuildInstance.transform.rotation);
        Collider2D collider = platformInstance.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        if (platformToBuildInstance != null)
        {
            Destroy(platformToBuildInstance);
            platformToBuildInstance = null;
        }

        platformButtons[platformIndex].interactable = false;
        StartCoroutine(ReEnableButtonAfterCooldown(platformIndex));
    }

    IEnumerator ReEnableButtonAfterCooldown(int platformIndex)
    {
        yield return new WaitForSeconds(platformCooldowns[platformIndex]);
        platformButtons[platformIndex].interactable = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectPlatform(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectPlatform(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectPlatform(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectPlatform(3);
        }

        if (platformToBuildInstance != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            platformToBuildInstance.transform.position = mousePosition;

            float rotationScroll = Input.GetAxis("Mouse ScrollWheel") * rotationSpeed;
            platformToBuildInstance.transform.Rotate(0f, 0f, -rotationScroll);

            if (Input.GetKeyDown(KeyCode.A))
            {
                platformToBuildInstance.transform.Rotate(0f, 0f, 45f);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                platformToBuildInstance.transform.Rotate(0f, 0f, -45f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    for (int i = 0; i < platformPrefabs.Length; i++)
                    {
                        if (platformToBuildPrefab == platformPrefabs[i])
                        {
                            BuildPlatform(mousePosition, i);
                            break;
                        }
                    }
                }
            }
        }
    }
}
