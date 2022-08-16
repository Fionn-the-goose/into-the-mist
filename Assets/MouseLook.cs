using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float m_Sensitivity = 100f;
    void Start(){
        
    }

    void Update(){
        float mouseX = Input.GetAxis("Mouse X") * m_Sensitivity * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * m_Sensitivity * Time.deltaTime;
    }
}
