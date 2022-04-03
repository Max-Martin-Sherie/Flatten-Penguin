using System;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Rect m_triggerZone;
    [SerializeField] private Door m_door;
    [SerializeField] private PlaqueDePressionAnimLocale m_plaque;

    // Update is called once per frame
    void Update()
    {
        Vector2 Pos2D = new Vector2(transform.position.x,transform.position.z);
        if (m_triggerZone.Contains(Pos2D))
        {
            m_door.Lower();
            m_plaque.BeginPressure();
        }
        else
        {
            m_door.Rise();
            m_plaque.EndPressure();
        }
    }

    private void OnDrawGizmosSelected()
    {
        
        Vector2 Pos2D = new Vector2(transform.position.x,transform.position.z);
        if (m_triggerZone.Contains(Pos2D))
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;
        
        Gizmos.DrawWireCube(new Vector3(m_triggerZone.x + m_triggerZone.width/2f,transform.position.y,m_triggerZone.y + m_triggerZone.height/2f),new Vector3(m_triggerZone.width,.2f,m_triggerZone.height));
        
    }
}
