using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI text;
    private float originSize = 80;
    private Color originColor = Color.white;
    [SerializeField] private float tweenSize = 100;
    [SerializeField] private Color tweenColor = Color.red;


    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        originSize = text.fontSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DOTween.To(() => originSize, x => text.fontSize = x, tweenSize, 0.25f);
        DOTween.To(() => originColor, x => text.color = x, tweenColor, 0.25f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.To(() => text.fontSize, x => text.fontSize = x, originSize, 0.5f);
        DOTween.To(() => text.color, x => text.color = x, originColor, 0.5f);
    }
}
