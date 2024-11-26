using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathButtonsEvents : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnClickContinue()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, 2);
        StartCoroutine(ChangeScene());
    }

    public void OnClickExit()
    {

    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(sceneName);
    }
}
