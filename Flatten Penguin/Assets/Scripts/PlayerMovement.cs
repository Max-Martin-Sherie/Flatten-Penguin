using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController m_characterController;
    [SerializeField] private Transform m_camera;
    [SerializeField] private Transform m_CameraTarget;
    [SerializeField] private Transform m_graphics3D;
    [SerializeField] private Transform m_graphics2D;
    [Space]
    [SerializeField, Min(0f)] private float m_speed = 9f;
    [SerializeField, Min(0f)] private float m_rotationSpeed = 70f;
    [SerializeField] private Vector2 m_offSet;
    [SerializeField] float m_PlayerHeightoffset = 1.5f;
    [SerializeField] private float m_cameraTransitionSpeed = 20f;
    [SerializeField] private float m_movementSmoothTime = .2f;
    [SerializeField] private float m_2DmovementSpeed = 3f;
    [SerializeField] private float m_gravityAcceleration = 30f;
    
    private Vector3 m_targetDirection;
    private Vector3 velocity = Vector3.zero;

    public enum Dimension
    {
        ThreeDee,
        TwoDee
    }
    
    public static Dimension m_dimension = Dimension.ThreeDee;

    public delegate void SwitchDimensionDelegator(Vector3 p_startPosition,Transform p_destination, int p_multiplier, Vector3 p_normal, bool p_IsExit);

    public static SwitchDimensionDelegator OnSwitchDimension;
    
    private void Awake()
    {
        OnSwitchDimension += OnSwitch;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_dimension)
        {
            case Dimension.ThreeDee:
                Update3D();
                break;
            case Dimension.TwoDee:
                Update2D();
                break;
        }
        if (!Input.GetKey(KeyCode.Space)) m_canSwitch = true;
    }

    #region ThreeDee

    private float m_gravity = 0f;
    void Update3D()
    {
        m_targetDirection = Vector3.SmoothDamp(m_targetDirection, GetInput3D(), ref velocity, m_movementSmoothTime);

        UpdateTargetPosition3D();

        m_gravity = m_characterController.isGrounded ? 0f : m_gravity + m_gravityAcceleration * Time.deltaTime;
        
        Vector3 displacement = m_targetDirection * (m_speed * Time.deltaTime) + Vector3.down * m_gravity;
        m_graphics3D.position = transform.position;
        m_graphics3D.LookAt(m_graphics3D.position + new Vector3(displacement.x,0,displacement.z) );
        m_characterController.Move(displacement);
        
        SetCameraPosition3D();
    }
    
    private void UpdateTargetPosition3D()
    {
        m_CameraTarget.position = Vector3.Lerp(m_CameraTarget.position, transform.position, Time.deltaTime * m_cameraTransitionSpeed);
        m_CameraTarget.rotation = Quaternion.Lerp(m_CameraTarget.rotation, transform.rotation, Time.deltaTime * m_cameraTransitionSpeed);
    }

    void SetCameraPosition3D()
    {
        m_camera.position = m_CameraTarget.position + m_CameraTarget.forward*m_offSet.x+ m_CameraTarget.up * m_offSet.y;
        m_camera.LookAt(transform.position + transform.up * m_PlayerHeightoffset);
    }
    
    Vector3 GetInput3D()
    {
        Vector3 targetDirection = Vector3.zero;
        
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            targetDirection = transform.forward;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            targetDirection -= transform.forward;

        if(Input.GetKey(KeyCode.D) ||  Input.GetKey(KeyCode.RightArrow))
        {
            if (targetDirection == Vector3.zero) targetDirection = (transform.forward + transform.right).normalized;
            transform.Rotate(transform.up * m_rotationSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) ||  Input.GetKey(KeyCode.LeftArrow))
        {
            if (targetDirection == Vector3.zero) targetDirection = (transform.forward - transform.right).normalized;
            transform.Rotate(transform.up * -m_rotationSpeed * Time.deltaTime);
        }

        return targetDirection;
    }

    #endregion

    private int m_multiplier = 1;
    private Vector3 m_normal;
    private bool m_isExit;
    
    void OnSwitch(Vector3 p_startPosition, Transform p_destination, int p_multiplier, Vector3 p_normal, bool p_isExit)
    {
        if(!m_canSwitch) return;
        m_destination = p_destination;
        m_multiplier = p_multiplier;
        m_isExit = p_isExit;
        
        switch (m_dimension)
        {
            case Dimension.ThreeDee:
                m_graphics3D.gameObject.SetActive(false);
                m_graphics2D.gameObject.SetActive(true);

                m_graphics2D.position = p_startPosition;
                m_graphics2D.LookAt(p_startPosition + p_normal);
                m_origin = p_startPosition;
                break;
            case Dimension.TwoDee:
                m_graphics3D.gameObject.SetActive(true);
                m_graphics2D.gameObject.SetActive(false);

                transform.position = p_startPosition;
                break;
        }

        m_dimension = m_dimension == Dimension.ThreeDee ? m_dimension = Dimension.TwoDee : m_dimension = Dimension.ThreeDee;
        m_canSwitch = false;
    }
    
    #region TwoDee

    private void UpdateTargetPosition2D()
    {
        m_CameraTarget.position = Vector3.Lerp(m_CameraTarget.position, m_graphics2D.position, Time.deltaTime * m_cameraTransitionSpeed);
        m_CameraTarget.rotation = Quaternion.Lerp(m_CameraTarget.rotation, m_graphics2D.rotation, Time.deltaTime * m_cameraTransitionSpeed);
    }

    void SetCameraPosition2D()
    {
        m_camera.position = m_CameraTarget.position + m_CameraTarget.forward*2.5f;
        m_camera.LookAt(m_graphics2D.position);
    }
    
    private Transform m_destination;
    private Vector3 m_origin;
    
    private bool m_canSwitch;

    [SerializeField] private float m_2dExitOffset =.25f; 
    
    void Update2D()
    {
        Vector3 destinationPos = m_destination.position;
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            direction += (destinationPos - m_origin).normalized * m_multiplier;
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            direction += (m_origin - destinationPos).normalized  * m_multiplier;
        
        m_graphics2D.position += direction * Time.deltaTime * m_2DmovementSpeed;

        float distanceBewteen = Vector3.Distance(destinationPos, m_origin);
        
        if (Vector3.Distance(m_graphics2D.position, m_origin) > distanceBewteen)
            m_graphics2D.position = destinationPos;
        

        if (Vector3.Distance(m_graphics2D.position, destinationPos) > distanceBewteen)
            m_graphics2D.position = m_origin;

        if(m_isExit)
        {
            if (m_canSwitch && Input.GetKeyDown(KeyCode.Space))
            {
                if (Vector3.Distance(m_graphics2D.position, m_origin) < m_2dExitOffset)
                    OnSwitchDimension?.Invoke(m_origin, null, 0, Vector3.zero, false);

                if (Vector3.Distance(m_graphics2D.position, destinationPos) < m_2dExitOffset)
                    OnSwitchDimension?.Invoke(destinationPos, m_destination, 0, Vector3.zero, false);
            }
        }
        else
        {
            if (Vector3.Distance(m_graphics2D.position, destinationPos) < m_2dExitOffset)
            {
                m_graphics2D.position = destinationPos;
                SwitchDimensionOnInputInTrigger switcher = m_destination.GetComponent<SwitchDimensionOnInputInTrigger>();

                m_origin = destinationPos;
                
                m_destination = switcher.m_destination;
                m_normal = switcher.m_normal;
                m_multiplier = switcher.m_invert? -1 : 1;
                
                
                m_graphics2D.LookAt(m_origin + m_normal);
            }
        }
        
        UpdateTargetPosition2D();
        SetCameraPosition2D();
    }

    #endregion
}