using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrtographicPlayer : MonoBehaviour
{

    private CharacterController m_Controller;
    public const float MOVEMENT_SNAPINESS = 0.5f;
    public const float SPEED = 12f;

    // Start is called before the first frame update
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
    }

    private static float Snap(float d) {
        return Mathf.Sign(d) * Mathf.Pow(Mathf.Abs(d), MOVEMENT_SNAPINESS);
    }

    private void UpdatePosition(){
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;

        Vector3 mouseTransform = new Vector3(mouseX, mouseY, 0f);

        Debug.Log(mouseTransform);
        transform.Rotate(mouseTransform);

        float x = Snap(Input.GetAxis("Horizontal"));
        float z = Snap(Input.GetAxis("Vertical"));

        Vector3 move = transform.right * x + transform.forward * z;
        m_Controller.Move(move * SPEED * Time.deltaTime);
    }

    // Update is called once per frame
    void Update(){
        UpdatePosition();
    }
}
