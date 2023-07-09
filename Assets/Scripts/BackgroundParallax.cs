using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code stolen from: https://www.youtube.com/watch?v=zit45k6CUMk
public class BackgroundParallax : MonoBehaviour
{
    private float length;
    private float startPosition;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;

    private void Start()
    {
        if (cam == null)
            cam = Camera.main.gameObject;

        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float movedDistance = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if (movedDistance > startPosition + length)
        {
            startPosition += length;
        }
        else if (movedDistance < startPosition - length)
        {
            startPosition -= length;
        }
    }
}
