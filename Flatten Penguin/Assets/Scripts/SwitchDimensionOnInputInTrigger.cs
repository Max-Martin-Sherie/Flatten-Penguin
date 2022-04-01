using UnityEngine;

public class SwitchDimensionOnInputInTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask m_playerLayer;

    [SerializeField] private Transform m_destination;
    private void OnTriggerStay(Collider p_other)
    {
        if(PlayerMovement.m_dimension == PlayerMovement.Dimension.TwoDee) return;

        if (m_playerLayer != (m_playerLayer | (1 << p_other.gameObject.layer))) return;
        
        if(Input.GetKeyDown(KeyCode.Space))
            PlayerMovement.OnSwitchDimension?.Invoke(transform.position, m_destination.position);
    }
    
    
}
