using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private float AttakingOffset;
    [SerializeField] private PlayerWallRuning wallRun;
    [SerializeField] private PlayerControler playerControler;
    private float timeValue;
    private void Start() {
        timeValue = AttakingOffset;
    }
    void Update() {
        timeValue -= Time.deltaTime;
        //Env Animations
        if (wallRun.isWallRuning) {

            if (wallRun.wallLeft) {
                Animator.SetBool("WallRunL",true);
            } else if (wallRun.wallRight) {
                Animator.SetBool("WallRunR",true);
            }
        } else {
            Animator.SetBool("WallRunR", false);
            Animator.SetBool("WallRunL", false);

        }
        if (Input.GetKey(KeyCode.LeftShift) && !playerControler.isInTheAir) {
            Animator.SetBool("IsRuning", true);
        } else {
            Animator.SetBool("IsRuning", false);
        }



        //KeysTrigerAnimations
        if (Input.GetKeyDown(KeyCode.F)) {
                Animator.SetTrigger("Inspect");
        }
        if (Input.GetMouseButtonDown(0)) {
            timeValue = AttakingOffset;
        }
        if (Input.GetMouseButtonDown(0) && Animator.GetInteger("Atack") >= 1) {
            if (Animator.GetInteger("Atack") > 2 || Animator.GetInteger("Atack") == 1) {
                Animator.SetInteger("Atack", 2);
            }else if(Animator.GetInteger("Atack") == 2) {
                Animator.SetInteger("Atack", 3);
            }

        } else if (Input.GetMouseButtonDown(0)){
            Animator.SetInteger("Atack", 1);
        } else if(timeValue <= 0){
            Animator.SetInteger("Atack", 0);
        }
    }
    
}
