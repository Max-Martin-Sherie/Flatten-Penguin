using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OnCollideHotDog : MonoBehaviour
{
    //variables
    [SerializeField, Tooltip("Link le hotDog")]
    private GameObject m_hotDog;
    [SerializeField, Tooltip("Link le gaz")]
    private GameObject m_gaz;

    private bool m_hotDogIsOn = true;
    
    [SerializeField, Tooltip("Hauteur maximale du gaz avant que le joueur ne perde")]
    private float m_maxHeightOverPlayer = 20f;
    [SerializeField, Tooltip("Vitesse a laquelle se propage le gaz")]
    private float m_gazSpeed;

    private Vector3 m_gazPosition;

    [SerializeField] private Volume m_volume;
    
    [SerializeField, Tooltip("Layer du player")]
    private LayerMask m_playerLayer;

    private void Start()
    {
        m_gazPosition = new Vector3(0f, m_gazSpeed, 0f) * Time.deltaTime;

        m_volume.weight = Mathf.Lerp(0,1, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_hotDogIsOn)
        {
            m_hotDog.transform.Rotate(new Vector3(0.5f, 0f, 0f));
            
            return;
        }

        //if(m_gaz.transform.position.y <=  m_maxHeightOverPlayer)
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
