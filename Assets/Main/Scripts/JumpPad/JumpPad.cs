using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider colider) {
        if(colider.tag == "Player") {
            Debug.Log(colider.attachedRigidbody);
            colider.attachedRigidbody.AddForce(colider.transform.up * 250, ForceMode.Impulse);
        }
    }
    void Update()
    {
        
    }
}
