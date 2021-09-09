using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    private bool isAtacking = true;
    public float timeValue;
    private void Start() {
        timeValue = 1.2f;
    }
    void Update() {
        timeValue -= Time.deltaTime;

        Debug.Log(timeValue);
        if (Input.GetKeyDown(KeyCode.F)) {
            if (!Animator.GetBool("LoEstaCriticando")) {
                Animator.SetBool("LoEstaCriticando", true);
            } else {
                Animator.SetBool("LoEstaCriticando", false);
            }
        }
        if (Input.GetMouseButtonDown(0)) {
            isAtacking = true;
            timeValue = 1.2f;
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
