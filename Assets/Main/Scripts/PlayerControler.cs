using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float XRotation;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Animator Animator;
    [SerializeField] private CapsuleCollider Collider;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float Sensitivty;
    [SerializeField] private float Jumpforce;
    private float height;
    private float radius;
    private float Velocity;
    private bool isScaling;
    private bool isWallRuning;
    private bool isInTheAir;
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        height = Collider.height;
        radius = Collider.radius;
    }

    private void Update() {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovePlayerCamera();
        MovePlayer();


    }
    private void FixedUpdate() {
       if (Physics.Raycast(PlayerBody.transform.position, PlayerBody.transform.TransformDirection(Vector3.forward), radius + 0.1f)) {
            if (Input.GetKey(KeyCode.Space)) {
                isScaling = true;
                PlayerBody.AddForce(Vector3.up * Jumpforce * 1.3f, ForceMode.Force);
            }
        } else {
            isScaling = false;
        }
        
        if (Physics.Raycast(PlayerBody.transform.position, PlayerBody.transform.TransformDirection(Vector3.left), radius + 0.1f) || Physics.Raycast(PlayerBody.transform.position, PlayerBody.transform.TransformDirection(Vector3.right), radius + 0.1f)) {
            if (isInTheAir) { 
                    PlayerBody.AddForce(Vector3.up * Jumpforce , ForceMode.Force);
                    isWallRuning = true;
            }
        } else {
            isWallRuning = false;
        }

    }

    private void MovePlayer() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            Velocity = RunSpeed;
        } else {
            Velocity = Speed;
        }
        if (Input.GetKey(KeyCode.LeftControl)) {
            Velocity -= Velocity / 2;
            Collider.height = height / 2;
        } else {
            Collider.height = height;
        }
        Debug.DrawRay(PlayerBody.transform.position, PlayerBody.transform.TransformDirection(Vector3.forward), Color.red);
        
        if (Physics.Raycast(PlayerBody.transform.position, Vector3.down, (Collider.height/2) + 0.1f)) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                        PlayerBody.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
                }
        } else {
            isInTheAir = true;
            Velocity = Speed;
        }
        
        if (isScaling) {
            Velocity = Speed / 3;
        }
        if (isWallRuning) {
            Velocity = RunSpeed * 1.1f;
        }
        Debug.Log("Velocity" + Velocity);
        Vector3 MoveVector = PlayerBody.transform.TransformDirection(PlayerMovementInput) * Velocity;
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);
    }
    private void MovePlayerCamera() {
        XRotation -= PlayerMouseInput.y * Sensitivty;

        PlayerBody.transform.Rotate(0f, PlayerMouseInput.x * Sensitivty, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(XRotation, 0, 0);
    }
}
