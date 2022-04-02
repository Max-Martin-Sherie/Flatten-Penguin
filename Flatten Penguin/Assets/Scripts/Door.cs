using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool m_lowering = false;
    public void Lower() => m_lowering = true;
    public void Rise() => m_lowering = false;

    private float m_upPosition;
    private float m_downPosition;
    [SerializeField]private float m_loweringDistance = 5f;
    [SerializeField]private float m_speed = 5f;
    
    private void Start()
    {
        m_upPosition = transform.position.y;
        m_downPosition = transform.position.y - m_loweringDistance;
    }

    private void Update()
    {
        if (m_lowering)
        {
            transform.position += Vector3.down * (m_speed * Time.deltaTime);
            if (transform.position.y < m_downPosition)
                transform.position = new Vector3(transform.position.x,m_downPosition,transform.position.z);
            return;
        }
        
        transform.position += Vector3.up * (m_speed * Time.deltaTime);
        if (transform.position.y > m_upPosition)
            transform.position = new Vector3(transform.position.x,m_upPosition,transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position ,transform.position + (Vector3.down * m_loweringDistance));
    }
}
