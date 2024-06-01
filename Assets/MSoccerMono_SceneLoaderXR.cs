using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MSoccerMono_SceneLoaderXR : MonoBehaviour
{

    public string m_sceneToLoadWithXrPlayer="XrPlayerScene";

    void Awake()
    {
        SceneManager.LoadScene(m_sceneToLoadWithXrPlayer, LoadSceneMode.Additive);
    }
    public void OnDestroy()
    {

        SceneManager.UnloadSceneAsync(m_sceneToLoadWithXrPlayer);
    }
}
