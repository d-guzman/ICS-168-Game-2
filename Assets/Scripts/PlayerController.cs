﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    public Camera mainCam;

    [Header("Player Movement Options")]
    [Tooltip("When this is true, player models will not rotate in the direction they are moving in.")]
    public bool strafeActive = false;
    [Tooltip("If strafing is active, the player's rotation will be inherited from the camera pivot.")]
    public Transform pivotTransform;
    [Tooltip("How quickly the player moves in world.")]
    public float moveSpeed = 8f;
    private float moveHori;
    private float moveVert;
    private Vector3 movement;
    private Rigidbody rb;

    [Tooltip("How much force should the player jump with? This is applied to the rigidbody as an impulse force.")]
    public float jumpStrength = 5f;
    private bool isGrounded = true;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        checkIfGrounded();
        movePlayer();
        jump();
    }

    //Private Functions
    private void movePlayer() {
        moveHori = Input.GetAxis("Horizontal");
        moveVert = Input.GetAxis("Vertical");

        Vector3 camForward = mainCam.transform.forward;
        Vector3 camRight = mainCam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;

        movement = camForward * moveVert + camRight * moveHori;
        movement.Normalize();

        if (movement.magnitude != 0 && !strafeActive)
        {
            Quaternion rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
            //transform.rotation = rotation;
        }
        else if (strafeActive)
        {
            Debug.Log("Cam Pivot Rotation: " + pivotTransform.rotation.eulerAngles);
            Quaternion rotation = Quaternion.Euler(0f, pivotTransform.rotation.eulerAngles.y, 0f);
            transform.rotation = rotation;
        }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void checkIfGrounded() {
        if (Physics.Raycast(transform.position, -transform.up, 1.001f))
        {
            rb.velocity = Vector3.zero;
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }
    }

    private void jump() {
        if ((Input.GetAxis("Jump") == 1) && isGrounded)
            rb.AddForce(transform.up * jumpStrength, ForceMode.Impulse);
    }
}
