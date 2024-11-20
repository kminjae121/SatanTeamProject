using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseHotKeyObject;
    public void ConttolPlayer(bool stop)
    {
        print("»Ð");
        if (GameObject.Find("PlayerCharacter"))
        {
            Player player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
            player.isStop = stop;
            print(player.isStop);
            if (stop)
            {
                Time.timeScale = 0;
            }
            else Time.timeScale = 1;
        }
    }

    public void EnalbeEscToPause()
    {
        pauseHotKeyObject.SetActive(true); 
    }

    GameObject FindObjectsWithTag(string tag)
    {
        Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform t in allTransforms)
        {
            if (t.hideFlags == HideFlags.None && t.CompareTag(tag))
            {
                return t.gameObject;
            }
        }
        return null;
    }
}
