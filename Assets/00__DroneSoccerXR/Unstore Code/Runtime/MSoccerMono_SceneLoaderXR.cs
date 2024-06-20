using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MSoccerMono_SceneLoaderXR : MonoBehaviour
{

    public string m_sceneToLoadWithXrPlayer="XrPlayerScene";


    public void SetAsXrProject(bool isXrProject)
    {
        if (isXrProject)
        {
            Debug.Log("Loading scene with XR player", this.gameObject);
            SceneManager.LoadScene(m_sceneToLoadWithXrPlayer, LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("Unloading scene with XR player", this.gameObject);
            SceneManager.UnloadSceneAsync(m_sceneToLoadWithXrPlayer);
        }
    }
 
}
