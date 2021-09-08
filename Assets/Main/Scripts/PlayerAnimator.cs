using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator Animator;

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (!Animator.GetBool("LoEstaCriticando")) {
                Animator.SetBool("LoEstaCriticando", true);
            } else {
                Animator.SetBool("LoEstaCriticando", false);
            }
        }
        if (Input.GetMouseButtonDown(0)){
            Animator.SetTrigger("Atack");
        }
    }
}
