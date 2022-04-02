using UnityEngine;

public class SwitcherGate : SwitcherAbstract
{
    [SerializeField] private Transform m_player;
    [SerializeField] private float m_minPlayerDistance = 2f;
    
    private void Update()
    {
        if(PlayerMovement.m_dimension == PlayerMovement.Dimension.ThreeDee)
        {
            if (Vector3.Distance(transform.position, m_player.position + Vector3.up) <= m_minPlayerDistance)
            {
                if (m_canPress && Input.GetKeyDown(KeyCode.Space))
                {
                    m_canPress = false;
                    PlayerMovement.EnterSwitcherGate?.Invoke(this);
                }
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Space)) m_canPress = true;
    }

    private bool m_canPress = true;
    public override void OnBegining()
    {
        if (m_canPress && Input.GetKeyDown(KeyCode.Space))
        {
            m_canPress = false;
            PlayerMovement.ExitSwitcherGate(transform.position);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, m_destination.transform.position);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_minPlayerDistance);
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,m_normal);
    }
#endif
}
