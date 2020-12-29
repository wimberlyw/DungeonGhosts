using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OfflinePlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;
    //public Camera theCamera;
    Vector3 ScaleToFlip = new Vector3(0, 0, 0);
    public bool facingLeft = false;

    void Start()
    {
        facingLeft = false;
    }
    // Update is called once per frame
    void Update()
    {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        
        animator.SetFloat("Speed", movement.sqrMagnitude);

       

    }

    void FixedUpdate()
    {
        //moves
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //Flip();

    }

   /* void Flip()
    {
        if ((movement.x > 0 && facingLeft) || (movement.x < 0 && !facingLeft))
        {
            facingLeft = !facingLeft;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }*/
}