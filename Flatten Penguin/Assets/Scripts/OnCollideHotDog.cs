using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideHotDog : MonoBehaviour
{
    //variables
    [SerializeField, Tooltip("Link le hotDog")]
    private GameObject m_hotDog;

    private bool m_hotDogIsOn = true;
    
    [SerializeField, Tooltip("Link le gaz")]
    private GameObject m_gaz;
    
    [SerializeField, Tooltip("Hauteur maximale du gaz avant que le joueur ne perde")]
    private float m_maxHeight = 20f;
    [SerializeField, Tooltip("Vitesse a laquelle se propage le gaz")]
    private float m_gazSpeed;

    private Vector3 m_gazPosition;
    
    [SerializeField, Tooltip("Layer du player")]
    private LayerMask m_playerLayer;

    private void Start()
    {
        m_gazPosition = new Vector3(0f, m_gazSpeed, 0f) * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_hotDogIsOn)
        {
            m_hotDog.transform.Rotate(new Vector3(0f, 0.5f, 0f));
            
            return;
        }

        if(m_gaz.transform.position.y <= m_maxHeight)
        m_gaz.transform.position += m_gazPosition;

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((m_playerLayer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            m_hotDogIsOn = false;
            m_hotDog.SetActive(false);
        }
    }
}
