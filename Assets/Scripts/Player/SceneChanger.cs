using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int sceneId; 

    public void ChangeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneId);
    }
}
