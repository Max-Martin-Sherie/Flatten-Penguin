using System;
using UnityEngine;

public class SetActiveOnCollide : MonoBehaviour
{
    [SerializeField] private LayerMask m_playerLayer;
    [SerializeField] private GameObject m_goToSetActive;
    private void OnTriggerEnter(Collider p_other)
    {
        if (m_playerLayer != (m_playerLayer | (1 << p_other.gameObject.layer))) return;
        
        m_goToSetActive.SetActive(true);
        Destroy(gameObject);
    }
}
