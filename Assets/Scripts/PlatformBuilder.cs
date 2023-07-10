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
    public Image[] cooldownImages;

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
        StartCoroutine(StartCooldown(platformIndex));
    }

    IEnumerator StartCooldown(int platformIndex)
    {
        float time = 0;
        cooldownImages[platformIndex].fillAmount = 1;
        while (time < platformCooldowns[platformIndex])
        {
            time += Time.deltaTime;
            cooldownImages[platformIndex].fillAmount -= 1.0f / platformCooldowns[platformIndex] * Time.deltaTime;
            print(cooldownImages[platformIndex].fillAmount);
            yield return null;
        }

        platformButtons[platformIndex].interactable = true;
        cooldownImages[platformIndex].fillAmount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (platformButtons[0].interactable)    
            SelectPlatform(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (platformButtons[1].interactable)
            SelectPlatform(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (platformButtons[2].interactable)
            SelectPlatform(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (platformButtons[3].interactable)
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
