using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpaceBar : MonoBehaviour
{
    private float m_range = 2.5f;
    public static bool m_inRange = false;
    private Transform m_player; 
    
    private void Start()
    {
        m_range = GetComponent<SwitcherGate>().m_minPlayerDistance;
        m_player = GetComponent<SwitcherGate>().m_player;
    }

    private void LateUpdate() => m_inRange = Vector3.Distance(m_player.position, transform.position) <= m_range || m_inRange;
}
