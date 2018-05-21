using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerXboxV1 : MonoBehaviour {

	public int playerNumber;
    public Camera mainCam;



    [Header("Player Movement Options")]
    [Tooltip("When this is true, player models will not rotate in the direction they are moving in.")]
    public bool strafeActive = false;
    [Tooltip("If strafing is active, the player's rotation will be inherited from the camera pivot.")]
    public Transform pivotTransform;
    [Tooltip("How quickly the player moves in world.")]
    public float moveSpeed = 8f;

	// public string 
    [Tooltip("The choosen suffix for what represents the button A on an xbox controller in the Unity Input Manager.")]
    public string AButtonInput = "A";
    [Tooltip("The choosen suffix for what represents the button B on an xbox controller in the Unity Input Manager.")]
	public string BButtonInput = "B";
    [Tooltip("The choosen suffix for what represents the button X on an xbox controller in the Unity Input Manager.")]
	public string XButtonInput = "X";
    [Tooltip("The choosen suffix for what represents the button Y on an xbox controller in the Unity Input Manager.")]
	public string YButtonInput = "Y";
    [Tooltip("The choosen suffix for what represents the left joystick's x/horizontal axis on an xbox controller in the Unity Input Manager.")]
	public string LeftJoystickXAxisInput = "LeftJoystickX";
    [Tooltip("The choosen suffix for what represents the left joystick's y/vertical axis on an xbox controller in the Unity Input Manager.")]
	public string LeftJoystickYAxisInput = "LeftJoystickY";
	// public string player1RightJoystickXAxisInput = "RightJoystickX";
	// public string player1RightJoystickYAxisInput = "RightJoystickY";

	// public string player1JumpAxis;

    [HideInInspector]
    public const string player1Str = "P1_";
    [HideInInspector]
    public const string player2Str = "P2_";
    [HideInInspector]
    public const string player3Str = "P3_";
    [HideInInspector]
    public const string player4Str = "P4_";

    private string playerPrefix;

    private float moveHori;
    private float moveVert;
    private Vector3 movement;
    private Rigidbody rb;

    [Tooltip("How much force should the player jump with? This is applied to the rigidbody as an impulse force.")]
    public float jumpStrength = 5f;
    private bool isGrounded = true;

        //These variables will be the positions of some guns 

    private Vector3 riflePosition = new Vector3(0.299f, -0.144f, 0.543f);

    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        
		// if (player1JumpAxis == null) {
		// 	player1JumpAxis = AButtonInput;
		// }

        switch (playerNumber)
        {
            case 0:
                Debug.Log("no player number was assigned, or you picked zero, which both are invalid.");
                break;
            case 1:
                playerPrefix = player1Str;
                break;
            case 2:
                playerPrefix = player2Str;
                break;
            case 3:
                playerPrefix = player3Str;
                break;
            case 4:
                playerPrefix = player4Str;
                break;
            // somehow chose more than 4 characters
            default:
                Debug.Log("The game does not support more than 4 players.");
                break;
        }
    }

    void FixedUpdate() {
        checkIfGrounded();
        movePlayer();
        jump();
    }

    //Private Functions
    private void movePlayer() {
        if (playerPrefix != null) {
            moveHori = Input.GetAxis(playerPrefix + LeftJoystickXAxisInput);
            moveVert = Input.GetAxis(playerPrefix + LeftJoystickYAxisInput);
        }

        Vector3 camForward = mainCam.transform.forward;
        Vector3 camRight = mainCam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;

        movement = camForward * moveVert + camRight * moveHori;
        if(moveVert != 0 && moveHori !=0)
        {
            //Debug.Log(playerPrefix+" is moving");
        }
        movement.Normalize();

        if (movement.magnitude != 0 && !strafeActive)
        {
            Quaternion rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
            //transform.rotation = rotation;
        }
        else if (strafeActive)
        {
            //Debug.Log("Cam Pivot Rotation: " + pivotTransform.rotation.eulerAngles);
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
        // if ((Input.GetAxis("A") == 1) && isGrounded)
        if (playerPrefix != null) {
            if ((Input.GetAxis(playerPrefix + AButtonInput) == 1) && isGrounded)
                rb.AddForce(transform.up * jumpStrength, ForceMode.Impulse);
        }
    }

    public string GetPlayerPrefix() {
        if (playerPrefix != null) {
            return playerPrefix;
        }
        else 
            return "not yet assigned";
    }
}
