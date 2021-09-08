using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
    [Header("Components")]
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private CapsuleCollider Collider;
    [SerializeField] private PlayerWallRuning wallRun;
    [Header("Sprinring")]
    [SerializeField] private float Speed;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float acceleration;
    [Header("Movement")]
    [SerializeField] private float MovementScale;
    [SerializeField] private float AirMovementScale;
    [SerializeField] private float Jumpforce;
    [Header("Drag")]
    [SerializeField] private float PlayerDrag;
    [SerializeField] private float PlayerAirDrag;
    [Header("Controls")]
    [SerializeField] private float Sensitivty;

    private Vector3 moveDirection;
    private Vector2 PlayerMouseInput;
    private float height,Velocity,horizontalMovement,verticalMovement;
    private bool isScaling, isWallRuning, isInTheAir;
 
    private float XRotation,YRotation;
    private float Xinput, Yinput;
    
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        height = Collider.height;

        PlayerBody.freezeRotation = true;
    }

    private void Update() {
        // Input Maping
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        moveDirection = PlayerBody.transform.forward * verticalMovement + PlayerBody.transform.right * horizontalMovement;

        isInTheAir = !Physics.Raycast(PlayerBody.transform.position, Vector3.down, height / 2 + 0.08f); 
        if(Input.GetKeyDown(KeyCode.Space) && !isInTheAir) {
            Jump();
        }
        ControlDrag();
        ControlSpeed();
        MovePlayerCamera();
    }
    private void FixedUpdate() {


        MovePlayer();
    }
    private void ControlDrag() {
        if (isInTheAir) {
            PlayerBody.drag = PlayerAirDrag;
        } else {
            PlayerBody.drag = PlayerDrag;
        }
    }
    private void ControlSpeed() {
        if (Input.GetKey(KeyCode.LeftShift) && !isInTheAir) {
            Velocity = Mathf.Lerp(Velocity, RunSpeed,acceleration * Time.deltaTime);
        } else {
            Velocity = Mathf.Lerp(Velocity, Speed, acceleration * Time.deltaTime);

        }
    }
    private void MovePlayer() {
        if (!isInTheAir) {
            PlayerBody.AddForce(moveDirection.normalized * Velocity * MovementScale, ForceMode.Acceleration); 
        } else {
            PlayerBody.AddForce(moveDirection.normalized * Velocity * MovementScale * AirMovementScale, ForceMode.Acceleration);
        }
    }
    
    private void MovePlayerCamera() {
        Xinput = Input.GetAxisRaw("Mouse X");
        Yinput = Input.GetAxisRaw("Mouse Y");

        YRotation += Xinput * Sensitivty;
        XRotation -= Yinput * Sensitivty;
        PlayerCamera.transform.localRotation = Quaternion.Euler(XRotation, 0, wallRun.tilt);
        PlayerBody.transform.rotation = Quaternion.Euler(0, YRotation, 0);
    }
    private void Jump() {
        PlayerBody.AddForce(PlayerBody.transform.up * Jumpforce, ForceMode.Impulse);
    }
}
