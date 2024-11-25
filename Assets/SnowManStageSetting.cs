using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SnowManStageSetting : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cutSceneCamera;

    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject fakeSnowMan;
    [SerializeField]
    private GameObject realSnowMan;
    [SerializeField]
    private GameObject fakeWall;

    public void MissingSnowMan()
    {
        Destroy(fakeSnowMan);
    }

    public void CutSceneStart()
    {
        cutSceneCamera.Priority = 10;
        cutSceneCamera.GetComponent<Animator>().enabled = true;
        player.isStop = true;
    }

    public void FinishCutScene()
    {
        cutSceneCamera.Priority = 0;
        player.isStop = false;
        fakeWall.SetActive(true);
        realSnowMan.SetActive(true);
    }
}
