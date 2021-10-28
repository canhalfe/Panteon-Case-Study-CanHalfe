using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutController : MonoBehaviour
{
    [SerializeField] float x1;
    [SerializeField] float x2;
    public float speed;
    private void Start()
    {
       
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 pos1 = new Vector3(x1, transform.position.y, transform.position.z);
        Vector3 pos2 = new Vector3(x2, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
