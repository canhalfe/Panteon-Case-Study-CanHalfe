using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStickController : MonoBehaviour
{
    public float rotationSpeed;
    void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
}
