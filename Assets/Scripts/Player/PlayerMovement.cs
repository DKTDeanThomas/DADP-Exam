using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cC;
    public Transform cam;

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


    private void Update()
    {
        Waypoints();
        float hz = Input.GetAxisRaw("Horizontal");
        float vt = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(hz, 0f, vt).normalized;

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

        if (Input.GetKey(KeyCode.J) && canJump)
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

    public GameObject Checkpoint1;
    public GameObject Checkpoint2;
    public GameObject Checkpoint3;
    public GameObject Checkpoint4;
    public GameObject Checkpoint5;

    public TMP_Text Checkpoint1Dist;
    public TMP_Text Checkpoint2Dist;
    public TMP_Text Checkpoint3Dist;
    public TMP_Text Checkpoint4Dist;
    public TMP_Text Checkpoint5Dist;



    void Waypoints()
    {
        if (Checkpoint1)

        {
            float chk1 = Vector3.Distance(Checkpoint1.transform.position, transform.position);
            chk1 = Mathf.RoundToInt(chk1);
            Checkpoint1Dist.text = "Meters from Checkpoint" + chk1;

            if (chk1 <= 3)
            {
                Destroy(Checkpoint1);
                Destroy(Checkpoint1Dist);
                Checkpoint2.SetActive(true);
            }
        }

        if (Checkpoint2)
            
        {
            float dist = Vector3.Distance(Checkpoint2.transform.position, transform.position);
            dist = Mathf.RoundToInt(dist);
            Checkpoint2Dist.text = "Meters from next Checkpoint" + dist;
            if (dist <= 2)
            {
                Destroy(Checkpoint2);
                Destroy(Checkpoint2Dist);
                Checkpoint3.SetActive(true);
            }
        }

        if (Checkpoint3)
            
        {
            float distance = Vector3.Distance(Checkpoint3.transform.position, transform.position);
            distance = Mathf.RoundToInt(distance);
            Checkpoint3Dist.text = "Meters from Checkpoint" + distance;

            if (distance <= 3)
            {
                Destroy(Checkpoint3);
                Destroy(Checkpoint3Dist);
                Checkpoint4.SetActive(true);
            }
        }

        if (Checkpoint4)
        {
            float chk4 = Vector3.Distance(Checkpoint4.transform.position, transform.position);
            chk4 = Mathf.RoundToInt(chk4);
            Checkpoint4Dist.text = "Meters from Checkpoint" + chk4;

            if (chk4 <= 3)
            {
                Destroy(Checkpoint4);
                Destroy(Checkpoint4Dist);
                Checkpoint5.SetActive(true);
            }
        }

        if (Checkpoint5)
        {
            float chk5 = Vector3.Distance(Checkpoint5.transform.position, transform.position);
            chk5 = Mathf.RoundToInt(chk5);
            Checkpoint5Dist.text = "Meters from Checkpoint" + chk5;

            if (chk5 <= 3)
            {
                Destroy(Checkpoint5);
                Destroy(Checkpoint5Dist);
            }
        }
        
    }
}

   

