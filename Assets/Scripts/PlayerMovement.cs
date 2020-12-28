using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5.0f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;
    public Camera theCamera;
    public NetworkAnimator netanim;

    void Start()
    {
        theCamera = GetComponentInChildren<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        //input
        if (isLocalPlayer)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horiontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            theCamera.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        //moves

        if (isLocalPlayer)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
