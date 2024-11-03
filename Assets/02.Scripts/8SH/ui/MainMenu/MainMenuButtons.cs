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
    [SerializeField] private GameObject doorLight;
    [SerializeField] private GameObject starLight;
    [SerializeField] private GameObject flashLight;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        fade = transform.parent.Find("Fade").GetComponent<Image>();
    }

    public void OnPlayButtonClick()
    {
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0, .5f);

        doorLight.SetActive(true);
        starLight.SetActive(false);
        flashLight.SetActive(false);

        mainCam.SetActive(false);
        fade.DOFade(1, 2).SetDelay(2);
    }

    public void OnSettingButtonClick()
    {

    }

    public void OnQuitButtonClick()
    {

    }
}
