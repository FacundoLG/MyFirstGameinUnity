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
    [SerializeField] private float wallRunDelay = 1;
    [Header("Camera")]
    [SerializeField] private float fov = 80;
    [SerializeField] private float wallRunfov = 100;
    [SerializeField] private float wallRunfovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;
    private Vector3 PlayerVelocity;
    public float Delta;
    bool wallLeft = false;
    bool wallRight = false;
    private float toFixAngle;
    //Exported
    public float tilt;
    public Quaternion wallAngle;
    public bool isWallRuning;
    
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
        wallRunDelay -= Delta;
        Debug.Log(wallRunDelay);
        Delta = Time.deltaTime;
        PlayerVelocity = PlayerBody.velocity - new Vector3(0,PlayerBody.velocity.y,0);
        
        CheckWall();
        if (wallLeft || wallRight) {
            if (CanWallRun()) {
                StartWallRun();
            }else {
                StopWallRuning();
            }
        } else {
            StopWallRuning();
        }



    }
    void StartWallRun() {
        isWallRuning = true;
        //Camera Efects
        PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);
        if (wallLeft) {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        } else if (wallRight) {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
        }
        
        PlayerBody.useGravity = false;
        
        if (leftWallHit.collider) {
            //Fix the angle for the Left RayCast
            toFixAngle = Vector3.Angle(-Orientation.transform.forward, leftWallHit.normal);
            if (toFixAngle > 90) {
                Orientation.localEulerAngles = new Vector3(0, Orientation.localEulerAngles.y - 0.5f, 0);
            } else if (toFixAngle < 90) {
                Orientation.localEulerAngles = new Vector3(0, Orientation.localEulerAngles.y + 0.5f, 0);
            }
        } else if (rightWallHit.collider) {

            //Fix the angle for the Right RayCast
            toFixAngle = Vector3.Angle(Orientation.transform.forward, rightWallHit.normal);
            if (toFixAngle > 90) {
                Orientation.localEulerAngles = new Vector3(0, Orientation.localEulerAngles.y - 0.5f, 0);
            } else if (toFixAngle < 90) {
                Orientation.localEulerAngles = new Vector3(0, Orientation.localEulerAngles.y + 0.5f, 0);
            }
        }
        PlayerBody.AddForce(Orientation.forward * WallRunJumpForce * Delta, ForceMode.Acceleration);
        if (Input.GetKeyDown(KeyCode.Space)) {
            wallRunDelay = 1;
            if (wallLeft) {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                //PlayerBody.velocity = new Vector3(PlayerBody.velocity.x, 0, PlayerBody.velocity.z);
                PlayerBody.AddForce(wallRunJumpDirection * WallRunJumpForce * 100, ForceMode.Force);
            } else if (wallRight) {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                //PlayerBody.velocity = new Vector3(PlayerBody.velocity.x, 0, PlayerBody.velocity.z);
                PlayerBody.AddForce(wallRunJumpDirection * WallRunJumpForce * 100, ForceMode.Force);
            }
            if (wallLeft || wallRight && PlayerBody.velocity.magnitude <= MaxVelocity) {
            PlayerBody.AddForce(PlayerCamera.transform.forward * WallRunJumpForce * 0.5f * Time.deltaTime, ForceMode.Acceleration);
            }
        } else if (wallRunDelay <= 0){
            PlayerBody.velocity = new Vector3(PlayerBody.velocity.x, 0f, PlayerBody.velocity.z);
        }

    }
    void StopWallRuning() {
        isWallRuning = false;
        PlayerBody.useGravity = true;
        PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }


}



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