using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GoodEnding : MonoBehaviour
{
    
    private Volume m_victoryVolume;
    [SerializeField] private float m_speed;

    private void Start() => m_victoryVolume = gameObject.GetComponent<Volume>();
    

    // Update is called once per frame
    void Update()
    {
        if(m_victoryVolume.weight > 0)
            m_victoryVolume.weight -= m_speed * Time.deltaTime;

        if (m_victoryVolume.weight < 0)
        {
            Destroy(this);
        }
    }
}
