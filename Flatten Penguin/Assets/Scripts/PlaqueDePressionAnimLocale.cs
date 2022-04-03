using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaqueDePressionAnimLocale : MonoBehaviour
{
    [SerializeField] private Vector3 m_initPos;
    [SerializeField] bool m_isPressed = false;
    [SerializeField] private float m_maxHeight;
    [SerializeField , Min(0f)] private float m_displacement;
    [SerializeField] private float m_speed;

    private void Start()
    {
        m_initPos = gameObject.transform.position;
    }

    private bool m_placed = true;
    
    // Update is called once per frame
    void Update()
    {
        if(m_placed) return;
        
        if (m_isPressed)
        {
            if (transform.position.y > m_initPos.y - m_displacement)
                transform.position += Vector3.down * (m_speed * 0.5f * Time.deltaTime);
            
            if(transform.position.y < m_initPos.y - m_displacement)
            {
                transform.position = m_initPos + Vector3.down * m_displacement;
                m_placed = true;
            }
            
            return;
        }

       
        if (transform.position.y < m_initPos.y)
        {
            transform.position += Vector3.up * (m_speed * Time.deltaTime);
            if (transform.position.y > m_initPos.y)
            {
                transform.position = m_initPos;
                m_placed = true;
            }
        }
    }

    [ContextMenu("A")]
    public void BeginPressure()
    {
        m_isPressed = true;
        m_placed = false;
    }
    
    [ContextMenu("B")]
    public void EndPressure()
    {
        m_isPressed = false;
        m_placed = false;
    }
    
    
}
