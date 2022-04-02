using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherGate : SwitcherAbstract
{
    [SerializeField] private Transform m_player;
    [SerializeField] private float m_minPlayerDistance = 2f;
    
    private void Update()
    {
        if(PlayerMovement.m_dimension == PlayerMovement.Dimension.TwoDee) return;
        
        if(Vector3.Distance(transform.position,m_player.position + Vector3.up) > m_minPlayerDistance) return;
        if(Input.GetKeyDown(KeyCode.Space))
            PlayerMovement.EnterSwitcherGate?.Invoke(this);
        m_canPress = false;
    }

    private bool m_canPress = false;
    public override void OnBegining()
    {
        if (!Input.GetKey(KeyCode.Space)) m_canPress = true;
        if (m_canPress && Input.GetKeyDown(KeyCode.Space)) PlayerMovement.ExitSwitcherGate(transform.position);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_minPlayerDistance);
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,m_normal);
    }
#endif
}
