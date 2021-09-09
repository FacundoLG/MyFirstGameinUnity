using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private float AttakingOffset;
    private bool isAtacking = true;
    
    private float timeValue;
    private void Start() {
        timeValue = AttakingOffset;
    }
    void Update() {
        timeValue -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F)) {
            if (!Animator.GetBool("LoEstaCriticando")) {
                Animator.SetBool("LoEstaCriticando", true);
            } else {
                Animator.SetBool("LoEstaCriticando", false);
            }
        }
        if (Input.GetMouseButtonDown(0)) {
            isAtacking = true;
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
