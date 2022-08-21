using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
//---------------------------------------------- private Member Variables ---------------------------------------------------

    private CharacterController m_Controller;
    private float m_MouseSensitivity = 100f;
    private Camera m_Camera;   
    private float m_xRotation = 0f;
    private bool m_isGrounded;
    private Vector3 m_Velocity;
    private float m_Stamina;
    private bool m_BlockSprint = false;
    private Coroutine m_StaminaRecoveryCoroutine = null;
//---------------------------------------------- public Member Variables ---------------------------------------------------
    public Transform groundCheck;
    public LayerMask groundLayer;
    public const float GRAVITY = -30f;
    public const float JUMP_HEIGHT = 2f;
    public RectTransform StaminaBar;

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

        PerformGroundCheck();

        if (m_isGrounded && m_Velocity.y < 0) {
            m_Velocity.y = -2f;
        }

        if (m_isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Jump");
            m_Velocity.y = Mathf.Sqrt(JUMP_HEIGHT * 2f * -GRAVITY);
        }
        
        m_Velocity.y += GRAVITY * Time.deltaTime;

        Vector3 move = transform.right * x + transform.forward * z;

        m_Controller.Move(m_Velocity * Time.deltaTime);
        m_Controller.Move(move * speed * Time.deltaTime);
    }
 
    public void PerformGroundCheck() {
        m_isGrounded = Physics.CheckSphere(groundCheck.position,
                GroundCheck.GROUND_CHECK_RADIUS,
                groundLayer
        );
    }

    private void UpdateStamina(float change_value) {
        float next_y = Mathf.Clamp((StaminaBar.transform.localScale.y + change_value), 0f, 1f);
        m_Stamina = next_y;
        Vector3 updated_scale = new Vector3(StaminaBar.transform.localScale.x, next_y, StaminaBar.transform.localScale.z);
        StaminaBar.transform.localScale = updated_scale;
    }

    void Update(){
        //Debug.Log("isGroundet: " + m_isGrounded);
        UpdateLookDirection();
        Debug.Log(m_Stamina);
        if(Input.GetKey(KeyCode.LeftControl)&& !m_BlockSprint){
            UpdateMovePosition(10f);
            UpdateStamina(-0.005f);
            if(m_Stamina <= 0.001){
                m_BlockSprint = true;
            }
        }
        else{
            UpdateMovePosition(5f);
            UpdateStamina(0.01f);
            if(m_Stamina >= 0.95){
                m_BlockSprint = false;
            }
        }
    }
}
