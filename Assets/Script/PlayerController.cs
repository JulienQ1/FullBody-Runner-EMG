using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 direction;
    public float speed;
    public int desiredLane = 1;
    public float laneDistance = 4;
    public int Gravity = 10;
    public int JumpForce = 5;
    public bool grounded;
    public bool IsWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame

   

    private void FixedUpdate()
    {
        grounded = controller.isGrounded;
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && controller.isGrounded)
        {
            Jump();

        }
        direction.z = speed;

        if (!controller.isGrounded)
        {
            direction.y += Gravity * Time.fixedDeltaTime * -1;
        }


        if (IsWalking)
        {
            controller.Move(direction * Time.fixedDeltaTime);
        }

        
        if (Input.GetKeyDown(KeyCode.LeftArrow) && desiredLane != 0)
        {
            desiredLane -= 1;
            ChangeLane();

        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && desiredLane != 2)
        {
            desiredLane += 1;
            ChangeLane();

        }
    }

    private void ChangeLane() 
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }

        if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.deltaTime);

    }

    private void Jump() 
    {
        direction.y = JumpForce;
    }
}
