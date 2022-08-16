using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController m_Controller;
    private float m_MouseSensitivity = 100f;
    private Camera m_Camera;   
    private float m_xRotation = 0f;

    void Start(){
        m_Controller = GetComponent<CharacterController>();
        m_Camera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
    }


    public void UpdateLookDirection(){
        float mouseX = Input.GetAxis("Mouse X") * m_MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -85f, 85f);

        m_Camera.transform.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void UpdateMovePosition(float speed){
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;

        m_Controller.Move(move * speed * Time.deltaTime);
    }
 
    void Update(){
        UpdateLookDirection();
        UpdateMovePosition(5f);
    }
}
