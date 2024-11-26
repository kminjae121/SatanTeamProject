using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI youCanSkipText;

    private float skipGage;
    [SerializeField]
    private Image skipGageBar;

    [SerializeField]
    private VideoPlayer introVideo;

    private void Start()
    {
        StartCoroutine(WaitRoutine());
    }

    private void Update()
    {
        skipGageBar.GetComponent<RectTransform>().localScale = new Vector3(skipGage, skipGageBar.transform.localScale.y, 0);
        if (Input.GetKey(KeyCode.Space))
        {
            if (skipGage > 8)
            {
                FirstIntroEnd();
                skipGageBar.gameObject.SetActive(false);
            }
            else
            {
                skipGage += Time.deltaTime * 2;
                skipGageBar.GetComponent<Image>().DOFade(1, skipGage);
            }
        }
        else
        {
            if (skipGage > 0)
            {
                skipGage -= Time.deltaTime * 3;
                skipGageBar.GetComponent<Image>().DOFade(0, skipGage);
            }
        }

        if (introVideo.time > 29f)
        {
            SceneManager.LoadScene("KYHMap");
        }
    }

    private void FirstIntroEnd()
    {
        SceneManager.LoadScene("KYHMap");
    }

    private IEnumerator WaitRoutine()
    {
        yield return new WaitForSeconds(1f);
        youCanSkipText.DOFade(1, 1);
        yield return new WaitForSeconds(3f);
        youCanSkipText.DOFade(0, 1);
    }
}
