using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private void Update()
    {
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }
}
