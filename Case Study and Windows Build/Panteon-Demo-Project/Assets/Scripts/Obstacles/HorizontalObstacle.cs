using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float xLimit;

    void FixedUpdate()
    {
        Vector3 pos1 = new Vector3(-xLimit, transform.position.y, transform.position.z);
        Vector3 pos2 = new Vector3(xLimit, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed / 50, 1.0f));
    }
}
