using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class VictoryPointBehavior : MonoBehaviour
{

    [SerializeField] private Transform m_player;
    [SerializeField] private float m_activationDistance;
    [SerializeField] private float m_winDistance;
    [SerializeField] private Volume m_victoryVolume;
    
    // Update is called once per frame
    void Update()
    {
        float winness = Mathf.Clamp((Vector3.Distance(transform.position, m_player.position) + m_winDistance)/ m_activationDistance,0,1);
        winness = Mathf.Abs(winness - 1f);

        m_victoryVolume.weight = Mathf.Lerp(0, 1, winness);
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
