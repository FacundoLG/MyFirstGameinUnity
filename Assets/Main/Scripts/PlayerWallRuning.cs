using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRuning : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private CapsuleCollider Collider;
    [Header("Detections")]
    [SerializeField] private float wallDistance = .5f;
    [SerializeField] private float minimumJumpHeight = 1.5f;
    [Header("wall Runing")]
    [SerializeField] private float WallRunGravity;
    [SerializeField] private float WallRunJumpForce;
    [Header("Camera")]
    [SerializeField] private float fov = 80;
    [SerializeField] private float wallRunfov = 100;
    [SerializeField] private float wallRunfovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;
    public float tilt;

    bool wallLeft = false;
    bool wallRight = false;
    bool isWallRunEnd = true;
    RaycastHit leftWallHit, rightWallHit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void CheckWall() {
        wallLeft = Physics.Raycast(PlayerBody.transform.position, -PlayerBody.transform.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(PlayerBody.transform.position, PlayerBody.transform.right, out rightWallHit, wallDistance);
    }
    bool CanWallRun() {
        return !Physics.Raycast(PlayerBody.transform.position, Vector3.down, minimumJumpHeight);
    }
    void Update()
    {
         
        CheckWall();
        if (CanWallRun()) {
            if (wallLeft) {
                StartWallRuning();
                Debug.Log("Wallruning on the left");
            }else if (wallRight) {
                StartWallRuning();
                Debug.Log("Wallruning on the right");
            } else {
                StopWallRuning();
            }
        } else {
            StopWallRuning();
        }
        
    }


    void StartWallRuning() {
        PlayerBody.useGravity = false;
        PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);
        if (wallLeft) {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        } else if (wallRight) {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
        } 


        if (Input.GetKeyDown(KeyCode.Space)) {
            if (wallLeft) {
                Vector3 wallRunJumpDirection = transform.up + PlayerCamera.transform.forward * 0.3f + leftWallHit.normal * 0.7f;
                PlayerBody.velocity = new Vector3(PlayerBody.velocity.x, 0, PlayerBody.velocity.z);
                PlayerBody.AddForce(wallRunJumpDirection * WallRunJumpForce * 100, ForceMode.Force);
            } else if (wallRight) {
                Vector3 wallRunJumpDirection = transform.up + PlayerCamera.transform.forward * 0.3f + rightWallHit.normal * 0.77f;
                PlayerBody.velocity = new Vector3(PlayerBody.velocity.x, 0, PlayerBody.velocity.z);
                PlayerBody.AddForce(wallRunJumpDirection * WallRunJumpForce * 100, ForceMode.Force);
            }
            StartCoroutine(WallRunDelay());
            //PlayerBody.AddForce(PlayerBody.transform.up * WallRunJumpForce, ForceMode.Impulse);
        } else if(isWallRunEnd){
           PlayerBody.velocity = new Vector3(PlayerBody.velocity.x, 0.1f, PlayerBody.velocity.z);
        }
        
    }
    void StopWallRuning() {
        PlayerBody.useGravity = true;
        isWallRunEnd = true;
        PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);

    }
    IEnumerator WallRunDelay() {
        isWallRunEnd = false;
        yield return new WaitForSeconds(2);
        isWallRunEnd = true;
    }
}
