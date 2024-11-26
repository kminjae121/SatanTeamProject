using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoSingleton<SceneChangeManager>
{
    private AsyncOperation asyncOperation;

    private void Start()
    {
        asyncOperation = SceneManager.LoadSceneAsync("DeathScene");
        asyncOperation.allowSceneActivation = false;
    }

    public void DeathScene()
    {
        asyncOperation.allowSceneActivation = true;
    }
}
