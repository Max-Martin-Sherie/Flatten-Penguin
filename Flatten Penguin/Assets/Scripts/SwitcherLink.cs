using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherLink : SwitcherAbstract
{
    [SerializeField] public SwitcherAbstract m_previousSwitcher;
    
    public override void OnBegining()
    {
        if (Input.GetKey(m_invert?KeyCode.Q: KeyCode.D)) PlayerMovement.Switching?.Invoke(m_previousSwitcher);
        if (Input.GetKey(m_invert?KeyCode.D: KeyCode.Q)) PlayerMovement.Switching?.Invoke(this);
    }
}
