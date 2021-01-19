using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightControl : MonoBehaviour
{

    public GameObject flashlight2D;
    public Rigidbody2D player;
    public float rotateSpeed = 0.3f;

    private Vector2 _playerLastPos;

    // Start is called before the first frame update
    void Start()
    {
        _playerLastPos = new Vector2(0, 0);

    }

    // Update is called once per frame
    void Update()
    {

        //Since we are not using physics to move the player, we can't find its velocity directly.
        //So I had to make my own by getting my lastPos - CurrentPos
        Vector2 _playerCurrentPos = player.transform.position;
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            //Vector2 playerMoveDirection = _playerLastPos - _playerCurrentPos;
            _playerLastPos = _playerCurrentPos;

            Vector2 verticalVec = Input.GetAxis("Vertical") * Vector2.up;
            Vector2 horizontalVec = Input.GetAxis("Horizontal") * Vector2.right;

            Vector2 playerMoveDirection = verticalVec + horizontalVec;

            //float curWalkAngle = Mathf.Atan2(-playerMoveDirection.x, playerMoveDirection.y) * Mathf.Rad2Deg;
            //Quaternion walkQuat = Quaternion.Euler(0, 0, curWalkAngle);

            Quaternion walkQuat = Quaternion.LookRotation(Vector3.forward, playerMoveDirection);

            flashlight2D.transform.rotation = Quaternion.Lerp(flashlight2D.transform.rotation, walkQuat, rotateSpeed);

        }
    }
}
