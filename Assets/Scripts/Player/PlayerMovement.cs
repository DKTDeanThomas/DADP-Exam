using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController cC;
    public Transform cam;
    public CinemachineVirtualCamera cinemachineVirtualCam;

    [SerializeField] public float walkSpeed = 6f;
    [SerializeField] public float sprintSpeed = 12f;
    [SerializeField] public float crouchSpeed = .1f;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private float smoothVelocity;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;

    private Vector3 velocity;
    private bool canJump = false;
   private bool canCrouch = true;

    private Vector3 crouchScale = new Vector3(1f, 0.5f, 1f);
    private Vector3 playerScale = new Vector3(1f, 1.5f, 1f);

    private float currentSpeed;

    [SerializeField] private GameObject focusPoint;

    public bool canRotate = true;

    private bool isMovementLocked = false;

    public void LockMovement(bool shouldLock)
    {
        isMovementLocked = shouldLock;
    }


    private void Update()
    {
        if (!enabled || isMovementLocked) return;
        float hz = Input.GetAxisRaw("Horizontal");
        float vt = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3 (hz, 0f, vt).normalized;

        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        //currentSpeed = Input.GetKey(KeyCode.C) ? crouchSpeed : walkSpeed;

        Vector3 moveDire = Vector3.zero;

        if (direction.magnitude >= 0.1f && canRotate)
        {
            float tAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tAngle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDire = Quaternion.Euler(0f, tAngle, 0f) * Vector3.forward;
        }

        if (Input.GetKey(KeyCode.C) && canCrouch)
        {
            currentSpeed = 0f;
            Crouch(true);

        }
        else if (!Input.GetKey(KeyCode.C) && !canCrouch)
        {
            Crouch(false);
        }
        
        //Debug.Log(currentSpeed);

        Vector3 currentVelocity = moveDire * currentSpeed;

        if (Input.GetButtonDown("Jump") && canJump)
        {
            Jump();
        }

        ApplyGravity();
        cC.Move(currentVelocity * Time.deltaTime);
        
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        cC.Move(velocity * Time.deltaTime);

        if (cC.isGrounded)
        {
            velocity.y = 0f;
            canJump = true;
        }
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        canJump = false;
    }

    private void Crouch(bool istru)
    {
        if (istru)
        {
            //Vector3 newPosition = focusPoint.transform.position;
            //newPosition.y -= .7f;
            //focusPoint.transform.position = newPosition;
            transform.localScale = crouchScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            canCrouch = false;

        }

        else if (!istru)
        {
            //Vector3 newPosition = focusPoint.transform.position;
            //newPosition.y += .7f;
            //focusPoint.transform.position = newPosition;
            transform.localScale = playerScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            canCrouch = true;
        }




    }


    public void SwitchCameraPriority(CinemachineVirtualCamera otherCamera, bool isDialogueActive)
    {
    if (isDialogueActive)
    {
        cinemachineVirtualCam.Priority = 9;
        otherCamera.Priority = 10;
    }
    else
    {
        cinemachineVirtualCam.Priority = 10;
        otherCamera.Priority = 9;
    }
    }
    

}
