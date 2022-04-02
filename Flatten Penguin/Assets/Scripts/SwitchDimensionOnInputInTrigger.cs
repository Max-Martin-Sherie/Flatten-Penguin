using System;
using UnityEngine;

public class SwitchDimensionOnInputInTrigger : MonoBehaviour
{
    [SerializeField] public Transform m_destination;
    [SerializeField] private Transform m_player;
    [SerializeField] private float m_minPlayerDistance = 2f;
    [SerializeField] public bool m_invert = false;
    [SerializeField] private bool m_isExit = false;
    [SerializeField] public Vector3 m_normal;
    
    public delegate void InRangeDelegator();
    public static InRangeDelegator OnInRange;
    private void Update()
    {
        if (!m_isExit) return;
        if(PlayerMovement.m_dimension == PlayerMovement.Dimension.TwoDee) return;
        
        if(Vector3.Distance(transform.position,m_player.position + Vector3.up) > m_minPlayerDistance) return;
        OnInRange?.Invoke();
        if(Input.GetKeyDown(KeyCode.Space))
            PlayerMovement.OnSwitchDimension?.Invoke(transform.position, m_destination, m_invert?-1:1, m_normal.normalized,m_isExit);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(m_isExit)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, m_minPlayerDistance);
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,m_normal);
    }
#endif
}
