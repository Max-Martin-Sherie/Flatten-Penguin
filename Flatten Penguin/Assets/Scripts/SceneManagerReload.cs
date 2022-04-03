using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerReload : MonoBehaviour
{
    void Update(){

        if(Input.GetKey(KeyCode.T))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
                
        }
    }
}
