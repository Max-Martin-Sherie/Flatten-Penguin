using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherLink : SwitcherAbstract
{
    [SerializeField] public SwitcherAbstract m_previousSwitcher;
    
    public override void OnBegining()
    {
        if (Input.GetKey(m_invert?KeyCode.Q: KeyCode.D) || Input.GetKey(m_invert?KeyCode.A: KeyCode.D) || Input.GetKey(m_invert?KeyCode.S: KeyCode.Z) || Input.GetKey(m_invert?KeyCode.S: KeyCode.W) || Input.GetKey(m_invert?KeyCode.LeftArrow: KeyCode.RightArrow) ||  Input.GetKey(m_invert?KeyCode.DownArrow: KeyCode.UpArrow))
            PlayerMovement.Switching?.Invoke(m_previousSwitcher);
        if (Input.GetKey(m_invert?KeyCode.D: KeyCode.Q) || Input.GetKey(m_invert?KeyCode.D: KeyCode.A) || Input.GetKey(m_invert?KeyCode.Z: KeyCode.S) || Input.GetKey(m_invert?KeyCode.W: KeyCode.S) || Input.GetKey(m_invert?KeyCode.RightArrow: KeyCode.LeftArrow) ||  Input.GetKey(m_invert?KeyCode.UpArrow: KeyCode.DownArrow))
            PlayerMovement.Switching?.Invoke(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, m_destination.transform.position);
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, m_normal);
    }
}
