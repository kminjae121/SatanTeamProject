using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Ending : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
    }

    private void Update()
    {
        if(videoPlayer.time > 23f)
        {
            StartCoroutine(FinishRoutine());
        }
    }

    private IEnumerator FinishRoutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("NewMainMenu");
    }
}
