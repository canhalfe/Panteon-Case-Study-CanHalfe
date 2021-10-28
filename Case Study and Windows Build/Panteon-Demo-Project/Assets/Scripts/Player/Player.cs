using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    Rigidbody rb;
    [SerializeField] bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("RotatingPlatform"))
        {
            Grounded();
        }
    }

    private void Grounded()
    {
        isGrounded = true;
        playerManager.playerState = PlayerManager.PlayerState.Move;
        //rb.useGravity = false;
        //rb.constraints = RigidbodyConstraints.FreezeAll;
        Destroy(this, 1f);
    }

}
