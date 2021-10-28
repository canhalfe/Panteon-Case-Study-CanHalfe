using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformController : MonoBehaviour
{
    public float rotationSpeed = 0.5f;
    void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }
}
