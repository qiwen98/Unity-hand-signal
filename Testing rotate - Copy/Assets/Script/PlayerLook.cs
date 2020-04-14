using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    float xRotation = 0f;
    [SerializeField]
    private Transform playerbody;
    
    public float mousesentivity = 200f;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerbody = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") *mousesentivity* Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") *mousesentivity* Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerbody.Rotate(Vector3.up * mouseX);
    }
}