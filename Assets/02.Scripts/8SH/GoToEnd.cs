using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GoToEnd : MonoBehaviour
{
    [SerializeField] private Image fade;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private string sceneName;

    private void Start()
    {
        print(fade);
    }

    public void End()
    {
        FindGameObjectByName("Boss").gameObject.SetActive(false);
        DOTween.KillAll();
        fade.DOFade(0, 2);
        bgm.DOFade(0, 2);
        //Invoke("ChangeScene", 3);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public GameObject FindGameObjectByName(string name)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }

        Debug.LogWarning($"GameObject with name '{name}' not found.");
        return null;
    }
}
