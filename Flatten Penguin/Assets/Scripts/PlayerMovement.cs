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

    public delegate void VoidDelegator(SwitcherAbstract p_current);
    public static VoidDelegator EnterSwitcherGate;
    public static VoidDelegator Switching;
    public delegate void VoidDelegator2( Vector3 p_position);
    public static VoidDelegator2 ExitSwitcherGate;
    
    private void Awake()
    {
        EnterSwitcherGate += OnEnterSwitcherGate;
        ExitSwitcherGate += OnExitSwitcherGate;
        Switching += Switch;
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
        
        Vector3 displacement = m_targetDirection * (m_speed * Time.deltaTime) + (Vector3.down * m_gravity * Time.deltaTime);
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

    #region TwoDee

    
    private int m_multiplier = 1;
    private Vector3 m_normal;
    
    private SwitcherAbstract m_currentSwitcher;
    private SwitcherAbstract m_nextSwitcher;
    
    
    private bool m_canSwitch;
    
    private Vector3 m_destination;
    private Vector3 m_origin;
    
    private void OnEnterSwitcherGate(SwitcherAbstract p_current)
    {
        m_currentSwitcher = p_current;
        m_nextSwitcher = p_current.m_destination;
        
        m_origin = m_currentSwitcher.transform.position;
        m_destination = m_nextSwitcher.transform.position;
        
        m_graphics2D.transform.position = m_origin;
        m_graphics2D.LookAt(m_origin + p_current.m_normal);


        if (m_normal == Vector3.up)
        {
            m_graphics2D.transform.Rotate(Vector3.up, 180f);
            Debug.Log("hey");
        }

        m_multiplier = p_current.m_invert ? -1 : 1;
        
        m_graphics3D.gameObject.SetActive(false);
        m_graphics2D.gameObject.SetActive(true);
        m_dimension = Dimension.TwoDee;
    }

    private void Switch(SwitcherAbstract p_current)
    {
        m_currentSwitcher = p_current;
        m_nextSwitcher = p_current.m_destination;
        
        m_origin = m_currentSwitcher.transform.position;
        m_destination = m_nextSwitcher.transform.position;
        
        m_graphics2D.LookAt(m_graphics2D.transform.position + p_current.m_normal);

        m_multiplier = p_current.m_invert ? -1 : 1;
    }
    
    
    private void OnExitSwitcherGate(Vector3 p_position)
    {
        transform.position = p_position;
        
        m_graphics3D.gameObject.SetActive(true);
        m_graphics2D.gameObject.SetActive(false);
        m_dimension = Dimension.ThreeDee;
    }
    
    private void UpdateTargetPosition2D()
    {
        m_CameraTarget.position = Vector3.Lerp(m_CameraTarget.position, m_graphics2D.position, Time.deltaTime * m_cameraTransitionSpeed);
        m_CameraTarget.rotation = Quaternion.Lerp(m_CameraTarget.rotation, m_graphics2D.rotation, Time.deltaTime * m_cameraTransitionSpeed);
    }

    void SetCameraPosition2D()
    {
        m_camera.position = m_CameraTarget.position + m_CameraTarget.forward*2.5f;
        m_camera.LookAt(m_graphics2D.position, m_normal);
    }
    

    [SerializeField] private float m_2dExitOffset =.25f; 
    
    void Update2D()
    {
        Vector3 destinationPos = m_destination;
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftArrow))
            direction += (destinationPos - m_origin).normalized * m_multiplier;
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.RightArrow))
            direction += (m_origin - destinationPos).normalized  * m_multiplier;
        
        m_graphics2D.position += direction * Time.deltaTime * m_2DmovementSpeed;

        float distanceBewteen = Vector3.Distance(destinationPos, m_origin);
        
        if (Vector3.Distance(m_graphics2D.position, m_origin) > distanceBewteen)
            m_graphics2D.position = destinationPos;
        

        if (Vector3.Distance(m_graphics2D.position, destinationPos) > distanceBewteen)
            m_graphics2D.position = m_origin;

        if (Vector3.Distance(m_graphics2D.position, m_origin) < m_2dExitOffset)
            m_currentSwitcher.OnBegining();
            
        if (Vector3.Distance(m_graphics2D.position, destinationPos) < m_2dExitOffset)
            m_nextSwitcher.OnBegining();

        
        
        UpdateTargetPosition2D();
        SetCameraPosition2D();
    }

    #endregion
}