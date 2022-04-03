using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class VictoryPointBehavior : MonoBehaviour
{

    [SerializeField] private Transform m_player;
    [SerializeField] private float m_activationDistance;
    [SerializeField] private float m_winDistance;
    [SerializeField] private Volume m_victoryVolume;

    [SerializeField] private int m_victorySceneIndex = 3;
    // Update is called once per frame
    void Update()
    {
        float value =
            Mathf.Min((Vector3.Distance(transform.position, m_player.position) - m_winDistance) / m_activationDistance,
                Vector3.Distance(transform.position, m_player.position) - m_winDistance);
        float winness = Mathf.Clamp(value,0,1);
        winness = Mathf.Abs(winness - 1f);

        m_victoryVolume.weight = Mathf.Lerp(0, 1, winness);
        if (winness >= 1) SceneManager.LoadScene(m_victorySceneIndex);
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(pos, m_winDistance);
        
        Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(pos, m_activationDistance);
    }
}
