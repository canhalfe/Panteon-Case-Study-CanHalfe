using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    void Update()
    {
        if (gameObject.transform.parent == null)
        {
            MoveToWall();
        }
    }

    private void MoveToWall()
    {
        Vector3 camPos = transform.position;
        Vector3 targetPos = new Vector3(0, 0.781f, 15.21f);
        Quaternion camRot = transform.rotation;
        Quaternion targetRot = Quaternion.identity;
        transform.position = Vector3.LerpUnclamped(camPos, targetPos, Time.deltaTime);
        transform.rotation = Quaternion.LerpUnclamped(camRot, targetRot, Time.deltaTime);
    }
}
