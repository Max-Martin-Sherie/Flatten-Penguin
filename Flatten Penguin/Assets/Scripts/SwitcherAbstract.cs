using UnityEngine;

public abstract class SwitcherAbstract : MonoBehaviour
{
    [SerializeField] public bool m_invert = false;
    [SerializeField] public Vector3 m_normal;
    [SerializeField] public SwitcherAbstract m_destination;

    public virtual void OnBegining(){}
}
