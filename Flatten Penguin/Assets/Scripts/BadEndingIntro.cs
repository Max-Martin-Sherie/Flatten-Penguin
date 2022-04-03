using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BadEndingIntro : MonoBehaviour
{

    [SerializeField] private RawImage m_greenScreen;
    [SerializeField, Min(0f)] private float m_fadeInSpeed;
    [SerializeField] private float m_waitTimeBefore = 2f;

    private void Start() => StartCoroutine(Wait());

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(m_waitTimeBefore);
        m_hasStarted = true;
    }

    private bool m_hasStarted = false;
    
    private void Update()
    {
        if(!m_hasStarted) return;
        
        Color color = m_greenScreen.color;
        color.a -= m_fadeInSpeed * Time.deltaTime;

        if (color.a < 0)
        {
            Destroy(m_greenScreen.gameObject);
            Destroy(gameObject);
            return;
        }
        
        m_greenScreen.color = color;
    }
}
