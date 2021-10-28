using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GirlController : MonoBehaviour
{
    bool isMoving = true;
    Vector3 startPos;
    [Header("Objects")]
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agent;
    [Header("Variables")]
    [SerializeField] float movementSpeed;

    void Start()
    {
        startPos = transform.position;
    }
    void FixedUpdate()
    {
        // agent is within a close range/touching target waypoint
        if (isMoving)
        {
            EnemyTowardNextPos();
        }
    }
    void EnemyTowardNextPos()
    {
        agent.SetDestination(target.position);
        var childScript = gameObject.GetComponentInChildren<PlayerAnimation>();
        childScript.anim.SetBool("idle", false);
        childScript.anim.SetBool("isRunning", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HalfDonut") || collision.gameObject.CompareTag("HalfDonutStick") ||
            collision.gameObject.CompareTag("StaticObstacle") || collision.gameObject.CompareTag("HorizontalObstacle") || collision.gameObject.CompareTag("Rotator"))
        {
            StartCoroutine(RestartPlayer());
        }
        if (collision.gameObject.CompareTag("RotatingStick"))
        {
            StartCoroutine(RestartPlayer());
        }
    }
    IEnumerator RestartPlayer()
    {
        gameObject.GetComponentInChildren<PlayerAnimation>().anim.SetBool("isHit", true);
        isMoving = false;
        agent.speed = 0f;
        yield return new WaitForSeconds(1f);
        transform.position = startPos;
        var childScript = gameObject.GetComponentInChildren<PlayerAnimation>();
        childScript.anim.Rebind();
        isMoving = true;
        agent.speed = 0.7f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinishLine"))
        {
            isMoving = false;
            agent.autoBraking = true;
            agent.speed = 0f;
            var childScript = gameObject.GetComponentInChildren<PlayerAnimation>();
            childScript.anim.SetBool("isRunning", false);
            childScript.anim.SetBool("idle", true);
        }
    }
}
