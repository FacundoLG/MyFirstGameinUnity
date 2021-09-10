using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRuning : MonoBehaviour {
    [Header("Components")]
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Transform Orientation;
    [SerializeField] private CapsuleCollider Collider;
    [Header("Detections")]
    [SerializeField] private float wallDistance = .5f;
    [SerializeField] private float minimumJumpHeight = 1.5f;
    [SerializeField] private float minimumVelocity = 1.5f;
    [Header("wall Runing")]
    [SerializeField] private float WallRunJumpForce;
    [SerializeField] private float MaxVelocity;

    [Header("Camera")]
    [SerializeField] private float fov = 80;
    [SerializeField] private float wallRunfov = 100;
    [SerializeField] private float wallRunfovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;
    private Vector3 PlayerVelocity;
    bool wallLeft = false;
    bool wallRight = false;
    //Exported
    public float tilt;
    public Quaternion wallAngle;
    public bool isWallRuning;

    public bool isWallRunEnd;
    RaycastHit leftWallHit, rightWallHit;
    void Start() {

    }

    // Update is called once per frame
    void CheckWall() {
        wallLeft = Physics.Raycast(Orientation.transform.position, -Orientation.transform.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(Orientation.transform.position, Orientation.transform.right, out rightWallHit, wallDistance);
    }
    bool CanWallRun() {
        if (PlayerVelocity.magnitude < minimumVelocity) {
            return false;
        }
        return !Physics.Raycast(PlayerBody.transform.position, Vector3.down, minimumJumpHeight);
    }
    void Update() {
        CheckWall();

        if (leftWallHit.collider) {
            isWallRuning = true;
            wallAngle = leftWallHit.transform.localRotation; 
        Debug.Log(wallAngle);

        }else if (rightWallHit.collider) {
            isWallRuning = true;
            wallAngle = rightWallHit.transform.localRotation;
        Debug.Log(wallAngle);
        }else if (Input.GetKeyDown(KeyCode.Space)) {
            isWallRuning = false;
        }


    }
}
     //    PlayerVelocity = PlayerBody.velocity - new Vector3(0,PlayerBody.velocity.y,0);
     //    if (CanWallRun()) {
     //        if (wallLeft) {
     //            StartWallRuning();
     //        }else if (wallRight) {
     //            StartWallRuning();
     //        } else {
     //            StopWallRuning();
     //        }
     //    } else {
     //        StopWallRuning();
     //    }
     //}


//void StartWallRuning() {
//    isWallRuning = true;
//    PlayerBody.useGravity = false;
//    PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);
//    if (wallLeft) {
//        tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
//    } else if (wallRight) {
//        tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
//    }
//    if (wallLeft || wallRight && PlayerBody.velocity.magnitude <= MaxVelocity) {
//        PlayerBody.AddForce(Orientation.transform.forward * WallRunJumpForce * 0.5f * Time.deltaTime, ForceMode.Acceleration);
//    }

//    if (Input.GetKeyDown(KeyCode.Space)) {
//        if (wallLeft) {
//            Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal ;
//            PlayerBody.velocity = new Vector3(PlayerBody.velocity.x, 0, PlayerBody.velocity.z);
//            PlayerBody.AddForce(wallRunJumpDirection * WallRunJumpForce * 100, ForceMode.Force);
//        } else if (wallRight) {
//            Vector3 wallRunJumpDirection = transform.up  + rightWallHit.normal ;
//            PlayerBody.velocity = new Vector3(PlayerBody.velocity.x, 0, PlayerBody.velocity.z);
//            PlayerBody.AddForce(wallRunJumpDirection * WallRunJumpForce * 100, ForceMode.Force);
//        }
//        if(wallLeft || wallRight && PlayerBody.velocity.magnitude <= MaxVelocity) {
//            PlayerBody.AddForce(PlayerCamera.transform.forward * 0.5f * WallRunJumpForce * 100, ForceMode.Force);
//        }
//        StartCoroutine(WallRunDelay());
//    } else if(isWallRunEnd){
//       PlayerBody.velocity = new Vector3(PlayerBody.velocity.x, 0f, PlayerBody.velocity.z);
//    }

//}
//void StopWallRuning() {
//    PlayerBody.useGravity = true;
//    isWallRunEnd = true;
//    PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
//    tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);

//}
//IEnumerator WallRunDelay() {
//    isWallRunEnd = false;
//    yield return new WaitForSeconds(1);
//    isWallRunEnd = true;

//}