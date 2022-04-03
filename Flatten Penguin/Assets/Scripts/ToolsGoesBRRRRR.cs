using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolsGoesBRRRRR : MonoBehaviour
{
    private delegate void heyheyehey();
    private static heyheyehey m_heyyy;
    private float HEHEHEHEYy;
    private float HEHEHEHEYynt;
    
    [ContextMenu("heyy?")]
    void heyy()
    {
        m_heyyy += heyyi;
    }

    void heyyi() => PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
    
    [ContextMenu("heyy!")]
    void heyyoo()
    {
        m_heyyy?.Invoke();
    }


    [ContextMenu("heyy'nt!")]
    void heyynt() => transform.position += transform.right * HEHEHEHEYy;
}
