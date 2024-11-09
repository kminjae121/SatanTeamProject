using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuButtons : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Image fade;
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject[] disactiveObjects;
    [SerializeField] private GameObject doorLight;
    [SerializeField] private AudioSource lightAudio;
    [SerializeField] private AudioSource bgm;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        fade = transform.parent.Find("Fade").GetComponent<Image>();
    }

    public void OnPlayButtonClick()
    {
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0, .5f);

        lightAudio.Play();
        bgm.Stop();
        for (int i = 0; i < disactiveObjects.Length; i++)
        {
            disactiveObjects[i].SetActive(false);
        }

        doorLight.SetActive(true);
        mainCam.SetActive(false);
        fade.DOFade(1, 2).SetDelay(2);
        StartCoroutine(SceneMove());
    }

    public void OnSettingButtonClick()
    {

    }

    public void OnQuitButtonClick()
    {

    }

    private IEnumerator SceneMove()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("MakingEvent");
    }
}
