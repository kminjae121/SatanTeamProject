using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Video;

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
    private VideoPlayer videoPlayer;

    public void MissingSnowMan()
    {
        Destroy(fakeSnowMan);
    }

    public void Audio()
    {
        AudioManager.Instance.PlaySound2D("CloseDoor", 0, false, SoundType.VfX);
    }

    public void CutSceneStart()
    {
        videoPlayer.Stop();
        cutSceneCamera.Priority = 10;
        cutSceneCamera.GetComponent<Animator>().enabled = true;
        player.isStop = true;
    }

    public void FinishCutScene()
    {
        cutSceneCamera.Priority = 0;
        player.isStop = false;
        realSnowMan.SetActive(true);
        realSnowMan.GetComponent<CryingAngel>().player = player.transform;
    }
}
