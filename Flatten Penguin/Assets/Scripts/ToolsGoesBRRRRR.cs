using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsGoesBRRRRR : MonoBehaviour
{
    private delegate void heyheyehey();
    private static heyheyehey m_heyyy;
    private float HEHEHEHEYy;
    
    [ContextMenu("heyy?")]
    void heyy()
    {
        m_heyyy += heyyi;
        HEHEHEHEYy = Random.Range(-.2f, .2f);
    }

    void heyyi() => transform.position += transform.forward * HEHEHEHEYy;
    
    [ContextMenu("heyy!")]
    void heyyoo()
    {
        m_heyyy?.Invoke();
    }
}
