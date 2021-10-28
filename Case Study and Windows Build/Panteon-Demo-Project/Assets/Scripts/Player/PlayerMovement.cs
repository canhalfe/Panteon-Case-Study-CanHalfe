using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float touchPosX;
    Vector3 startPos;
    public bool isMoving = true;
    [Header("Objects")]
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Transform child;
    [SerializeField] Camera cam;
    [SerializeField] GameObject wall;
    [SerializeField] ParticleSystem finishEffect;
    [SerializeField] Texture2D cursor;
    [SerializeField] Text percentText;
    [SerializeField] Text infoText;
    [SerializeField] TMP_Text rankText;
    [Header("Variables")]
    [SerializeField] float movementSpeed;
    [SerializeField] float controlSpeed;
    [SerializeField] float horizontalLimit;
    [SerializeField] float crashForce;
    [SerializeField] bool isTouching;

    void Start()
    {
        startPos = new Vector3(0, 0, 0);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
        }
        Moving();
    }

    public void Moving()
    {
        if (isMoving)
        {
            if (playerManager.playerState == PlayerManager.PlayerState.Move)
            {
                transform.position += Vector3.forward * movementSpeed * Time.fixedDeltaTime;
            }
            if (isTouching)
            {
                touchPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
            }
            transform.position = new Vector3(Mathf.Clamp(touchPosX, -horizontalLimit, horizontalLimit), transform.position.y, transform.position.z);
            var childScript = gameObject.GetComponentInChildren<PlayerAnimation>();
            childScript.anim.SetBool("isRunning", true);
            childScript.anim.SetBool("idle", false);
        }
    }

    private void GetInput()
    {
        if (Input.GetMouseButton(0))
            isTouching = true;
        else
            isTouching = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HalfDonut") || collision.gameObject.CompareTag("HalfDonutStick") ||
            collision.gameObject.CompareTag("StaticObstacle") || collision.gameObject.CompareTag("HorizontalObstacle") || collision.gameObject.CompareTag("Rotator") || collision.gameObject.CompareTag("Water"))
        {
            StartCoroutine(RestartPlayer());
        }
        if (collision.gameObject.CompareTag("RotatingStick"))
        {
            StartCoroutine(RestartPlayer());
            rb.AddForce(Vector3.back.normalized * crashForce, ForceMode.Acceleration);
            rb.AddForce(Vector3.up.normalized * crashForce, ForceMode.Acceleration);
        }
    }

    IEnumerator RestartPlayer()
    {
        gameObject.GetComponentInChildren<PlayerAnimation>().anim.SetBool("isHit", true);
        isMoving = false;
        yield return new WaitForSeconds(1f);
        transform.position = startPos;
        var childScript = gameObject.GetComponentInChildren<PlayerAnimation>();
        childScript.transform.rotation = Quaternion.identity;
        childScript.anim.Rebind();
        child.transform.position = new Vector3(0, 0.01f, 0);
        touchPosX = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinishLine"))
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
            isMoving = false;
            var childScript = gameObject.GetComponentInChildren<PlayerAnimation>();
            childScript.anim.SetBool("isRunning", false);
            childScript.anim.SetBool("idle", true);
            cam.transform.parent = null;
            wall.gameObject.SetActive(true);
            percentText.gameObject.SetActive(true);
            infoText.gameObject.SetActive(true);
            rankText.gameObject.SetActive(false);
            finishEffect.gameObject.SetActive(true);
            finishEffect.Stop();
            Destroy(this, 1f);
        }
    }
}
