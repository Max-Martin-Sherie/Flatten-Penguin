using UnityEngine;

public abstract class SwitcherAbstract : MonoBehaviour
{
    [SerializeField] public bool m_invert = false;
    [SerializeField] public Vector3 m_normal;
    [SerializeField] public SwitcherAbstract m_destination;

    public virtual void OnBegining(){}
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, m_destination.transform.position);
    }
    #endif
}
