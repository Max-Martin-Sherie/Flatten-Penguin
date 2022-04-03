using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnCollideHotDog : MonoBehaviour
{
    //variables
    [SerializeField, Tooltip("Link le hotDog")]
    private GameObject m_hotDog;
    [SerializeField, Tooltip("Link le gaz")]
    private GameObject m_gaz;

    [SerializeField] private ParticleSystem m_pickHotDog;
    [SerializeField] float m_timerPS = 0.4f;
    private bool m_psOn = false;

    private bool m_hotDogIsOn = true;
    
    [SerializeField, Tooltip("Hauteur maximale du gaz avant que le joueur ne perde")]
    private float m_maxHeightOverPlayer = 2.3f;
    [SerializeField, Tooltip("Vitesse a laquelle se propage le gaz")]
    private float m_gazSpeed;

    private Vector3 m_gazDisplacement;

    [SerializeField] private Volume m_volume;
    [SerializeField] private RawImage m_greenScreen;
    
    [SerializeField, Tooltip("Layer du player")]
    private LayerMask m_playerLayer;

    [SerializeField] private int m_badEndingSceneIndex = 2;

    private Transform m_player;

    private float m_maxLeft = 61f;

    private void Start()
    {
        m_gazDisplacement = new Vector3(0f, m_gazSpeed, 0f);
    }
    
    bool m_escaped = false;

    // Update is called once per frame
    void Update()
    {
        if (m_hotDogIsOn)
        {
            m_hotDog.transform.RotateAround(Vector3.up, 4f * Time.deltaTime);
            return;
        }

        float deathness = Mathf.Clamp((m_player.position.y + 2.3f - m_gaz.transform.position.y) / m_maxHeightOverPlayer, 0f, 1f);

        deathness = Mathf.Abs(deathness - 1f);
        if(!m_escaped)
        {
            if (m_player.position.x > m_maxLeft)
            {
                m_escaped = true;
                m_gazDisplacement = -m_gazDisplacement;
            }

            if (deathness >= 1f) SceneManager.LoadScene(m_badEndingSceneIndex);
        }
        
        m_gaz.transform.position += m_gazDisplacement * Time.deltaTime;
        
        m_volume.weight = deathness;
        Color color = m_greenScreen.color;
        color.a = Mathf.Pow(deathness, 16f);
        m_greenScreen.color = color;

        if (m_psOn)
        {
            m_timerPS -= Time.deltaTime;
            if (m_timerPS < 0) m_psOn = false;
            return;
        }
        m_pickHotDog.Stop();
    }

    private void OnTriggerEnter(Collider p_other)
    {
        if ((m_playerLayer.value & (1 << p_other.transform.gameObject.layer)) > 0)
        {
            
            m_player = p_other.transform;
            m_hotDogIsOn = false;
            m_hotDog.SetActive(false);
            m_pickHotDog.Play();
            m_psOn = true;

        }
    }
}
