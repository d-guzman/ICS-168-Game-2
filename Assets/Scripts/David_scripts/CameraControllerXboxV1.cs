using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerXboxV1 : MonoBehaviour {
[Tooltip("This is the camera that is under the control of the player.")]
    public Transform currentCamera;

	public string player1RightJoystickXAxisInput = "RightJoystickX";
	public string player1RightJoystickYAxisInput = "RightJoystickY";

	public float sensitivity = 50.0f;

    [Header("Turn on First-Person")]
    [Tooltip("This determines whether the camera is a first person camera or a third person camera. If this is true, the camera will be in first-person perspective")]
    public bool firstPerson = false;

    [Header("Variables for First-Person Perspective")]
    [Tooltip("This is the object that the camera will focus on when in first-person perspective.")]
    public Transform FP_CameraFocus;

    [Header("Variables for Third-Person Perspective")]
    [Tooltip("This is the object that the camera will focus on when in third-person perspective.")]
    public Transform TP_CameraFocus;
    [Tooltip("When using third-person perspective, this is the distance the camera will be set at.")]
    public float CameraDistance = -10f;

    private Vector3 CameraPosition;
    private Quaternion nextRotation;
    private float rotateX;   // Numbers that are used to make the Quaternion 
    private float rotateY;

    void Start() {
        if (CameraDistance > 0)
            CameraDistance = -CameraDistance;
        if (!firstPerson)
            CameraPosition = new Vector3(0f, 0f, CameraDistance);
        else
            CameraPosition = Vector3.zero;
    }

    void LateUpdate() {
        updatePivotPosition();
        rotateCamera();
        zoomCamera();
    }

    //Private Functions
    private void zoomCamera() {
        if (!firstPerson)
        {
            RaycastHit hitInfo;
            int layerMask = 1 << 8;
            layerMask = ~layerMask;

            if (Physics.Raycast(transform.position, -transform.forward, out hitInfo, 10f, layerMask))
            {
                Vector3 CurrentPosition = currentCamera.localPosition;
                Vector3 NextPosition = new Vector3(0, 0, -hitInfo.distance);
                currentCamera.localPosition = Vector3.Lerp(CurrentPosition, NextPosition, 1f);
            }
            else
            {
                Vector3 CurrentPosition = currentCamera.localPosition;
                currentCamera.localPosition = Vector3.Lerp(CurrentPosition, CameraPosition, 1f);
            }
        }

        else
        {
            currentCamera.localPosition = Vector3.zero;
        }
    }

    private void updatePivotPosition() {
        if(!firstPerson)
            transform.position = TP_CameraFocus.position;
        else
            transform.position = FP_CameraFocus.position;
    }

    private void rotateCamera() {
        rotateX += Input.GetAxis(player1RightJoystickXAxisInput) * Time.deltaTime * sensitivity;
        rotateY += Input.GetAxis(player1RightJoystickYAxisInput) * Time.deltaTime * sensitivity;
        if (rotateX == 0.0f && rotateY == 0.0f)
            Debug.Log("No player 1 right joystick movement");
        rotateY = Mathf.Clamp(rotateY, -89.5f, 89.5f);

        nextRotation = Quaternion.Euler(rotateY, rotateX, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, nextRotation, 1f);
    }
}
