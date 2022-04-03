using UnityEngine;

public class soundonSpeed : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > 10f)
        {
            m_audioSource.volume = 1;
            return;
        }
        
        m_audioSource.volume = 0;
    }
}
