using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

public class IntoEndingScene : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cinemachine;
    [SerializeField]
    private Image fade;
    [SerializeField] GameObject monster;

    public void InDoor()
    {
        monster.SetActive(false);
        cinemachine.gameObject.SetActive(true);
        fade.DOFade(1, 2);
    }
}
