using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private CanvasGroup buttonCanvas;
    [SerializeField] private AudioSource deathBGM;
    private Image background;

    WaitForSecondsRealtime waitFor1Second = new WaitForSecondsRealtime(1);

    private Color originColor;

    private void Start()
    {
        background = GetComponent<Image>();
        originColor = background.color;
        deathBGM = GetComponent<AudioSource>();

        PopUpDeathScreen();
    }

    public void PopUpDeathScreen()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        background.color = originColor;
        background.DOColor(Color.black, 2);
        yield return waitFor1Second;
        deathBGM.Play();
        deathText.DOFade(1, 2);
        yield return waitFor1Second;
        buttonCanvas.DOFade(1, 2);
    }
}
