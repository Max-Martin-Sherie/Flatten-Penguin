using UnityEngine;

public class RandomLightingIntensity : MonoBehaviour
{
    private float[] m_intensity = new float[4];
    private float[] m_speed = new float[4];

    private Light m_light;
    
    void Start()
    {
        m_light = GetComponent<Light>();
        
        for (int i = 0; i < 4; i++)
        {
            m_intensity[i] = Random.Range(.4f, .5f);
            m_speed[i] = Random.Range(10f, 20f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float intensity = 0;
        for (int i = 0; i < 4; i++)
            intensity += ((Mathf.Cos(Time.timeSinceLevelLoad * m_speed[i]) + 1f) / 2f) * m_intensity[i];
        m_light.intensity = intensity;
    }
}
