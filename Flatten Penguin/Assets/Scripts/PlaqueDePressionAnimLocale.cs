using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaqueDePressionAnimLocale : MonoBehaviour
{
    Vector3 m_initPos;
    bool m_isPressed = false;
    [SerializeField , Min(0f)] private float m_displacement;
    [SerializeField] private float m_speedDown;
    [SerializeField] private float m_speedUp;
    
    
    private void Start() => m_initPos = gameObject.transform.position;
    private bool m_placed = true;
    
    // Update is called once per frame
    void Update()
    {
        if(m_placed) return;
        
        if (m_isPressed)
        {
            if (transform.position.y > m_initPos.y - m_displacement)
                transform.position += Vector3.down * (m_speedDown * 0.5f * Time.deltaTime);
            
            if(transform.position.y < m_initPos.y - m_displacement)
            {
                transform.position = m_initPos + Vector3.down * m_displacement;
                m_placed = true;
            }
            
            return;
        }

       
        if (transform.position.y < m_initPos.y)
        {
            transform.position += Vector3.up * (m_speedUp * Time.deltaTime);
            if (transform.position.y > m_initPos.y)
            {
                transform.position = m_initPos;
                m_placed = true;
            }
        }
    }

    public void BeginPressure()
    {
        m_isPressed = true;
        m_placed = false;
    }
    
    public void EndPressure()
    {
        m_isPressed = false;
        m_placed = false;
    }
    
    
}
