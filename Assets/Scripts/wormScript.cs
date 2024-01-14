using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wormScript : MonoBehaviour
{

    [SerializeField]
    private float move_speed = 5;
    [SerializeField]
    private float rotationSpeed = 400;

    float movementX;
    float movementY;

    public bool canMove; 

    Rigidbody2D rb;

    void Start()
    {
        movementX = 0;
        movementY = 0;

        canMove = true; 
    }

    void Update()
    {


        if (canMove)
        {
            Vector3 moveDir = new Vector3(movementX, movementY).normalized;
            transform.position += moveDir * move_speed * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.W))
            {
                movementY += 1;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                movementY -= 1;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                movementY -= 1;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                movementY += 1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                movementX += 1;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                movementX -= 1;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                movementX -= 1;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                movementX += 1;
            }
            if (moveDir != Vector3.zero)
            {

                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            Debug.Log("Cannot move");
            gameObject.SetActive(false);
        }
    }

    public void DisablePlayerMovement()
    {
        canMove = false;
    }
}
