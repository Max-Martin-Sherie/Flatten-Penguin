using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController m_characterController;
    [SerializeField] private Transform m_camera;
    [SerializeField] private Transform m_CameraTarget;
    [SerializeField] private Transform m_graphics;
    [Space]
    [SerializeField, Min(0f)] private float m_speed = 9f;
    [SerializeField, Min(0f)] private float m_rotationSpeed = 70f;
    [SerializeField] private Vector2 m_offSet;
    [SerializeField] float m_PlayerHeightoffset = 1.5f;
    [SerializeField] private float m_cameraTransitionSpeed = 20f;
    [SerializeField] private float m_movementSmoothTime = .2f;

    private Vector3 m_targetDirection;
    private Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        m_targetDirection = Vector3.SmoothDamp(m_targetDirection, GetInput(), ref velocity, m_movementSmoothTime);

        UpdateTargetPosition();
        
        SetCameraPosition();

        Vector3 displacement = m_targetDirection * (m_speed * Time.deltaTime);
        m_graphics.position = transform.position;
        m_graphics.LookAt(m_graphics.position + displacement);
        m_characterController.Move(displacement);
    }

    private void UpdateTargetPosition()
    {
        m_CameraTarget.position = Vector3.Lerp(m_CameraTarget.position, transform.position, Time.deltaTime * m_cameraTransitionSpeed);
        m_CameraTarget.rotation = Quaternion.Lerp(m_CameraTarget.rotation, transform.rotation, Time.deltaTime * m_cameraTransitionSpeed);
    }

    void SetCameraPosition()
    {
        m_camera.position = m_CameraTarget.position + m_CameraTarget.forward*m_offSet.x+ m_CameraTarget.up * m_offSet.y;
        m_camera.LookAt(transform.position + m_CameraTarget.up * m_PlayerHeightoffset);
    }
    
    Vector3 GetInput()
    {
        Vector3 targetDirection = Vector3.zero;
        
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            targetDirection = transform.forward;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            targetDirection -= transform.forward;

        if(Input.GetKey(KeyCode.D) ||  Input.GetKey(KeyCode.RightArrow))
        {
            if (targetDirection == Vector3.zero) targetDirection = (transform.forward/2f + transform.right).normalized;
            transform.Rotate(transform.up * m_rotationSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) ||  Input.GetKey(KeyCode.LeftArrow))
        {
            if (targetDirection == Vector3.zero) targetDirection = (transform.forward/2f - transform.right).normalized;
            transform.Rotate(transform.up * -m_rotationSpeed * Time.deltaTime);
        }

        return targetDirection;
    }
}
