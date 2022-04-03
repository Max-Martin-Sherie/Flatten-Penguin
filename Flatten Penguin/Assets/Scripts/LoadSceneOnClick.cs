using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    [SerializeField] private int m_nextSceneIndex;

    void Update() { if (Input.GetKeyDown(KeyCode.Mouse0)) SceneManager.LoadScene(m_nextSceneIndex); }
}