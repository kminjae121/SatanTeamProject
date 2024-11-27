using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChasePart : MonoBehaviour
{
    private GameObject playerObj;
    private Player player;

    private void Start()
    {
        playerObj = FindGameObjectByName("PlayerCharacter(AudioInput)").gameObject;
        player = playerObj.GetComponent<Player>();
        player.isStop = true;
    }

    public void ChaseStart()
    {
        player.isStop = false;
        playerObj.GetComponent<PlayerCam>().enabled = true;
        FindGameObjectByName("Timeline").gameObject.SetActive(false);
        FindGameObjectByName("StartTrigger").GetComponent<BossChasePathSetter>().Active();
        FindGameObjectByName("ScreenCanvas").gameObject.SetActive(true);
    }

    public void PlayBGM()
    {
        FindGameObjectByName("ChaseStart").GetComponent<AudioSource>().Play();
    }

    public GameObject FindGameObjectByName(string name)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }

        Debug.LogWarning($"GameObject with name '{name}' not found.");
        return null;
    }
}
