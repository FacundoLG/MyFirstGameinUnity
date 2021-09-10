using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    
    [Header("Parameters")]
    [SerializeField] private float movementAmount;
    [SerializeField] private float smoothAmount;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = -Input.GetAxis("Mouse X") * movementAmount;
        float moveY = -Input.GetAxis("Mouse Y") * movementAmount;

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, smoothAmount * Time.deltaTime);
    }
}
