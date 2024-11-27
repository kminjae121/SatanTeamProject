using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosseKillTrigger : MonoBehaviour
{
    private GameObject jumpscareCamera;
    private GameObject trapGage;
    private void Start()
    {
        try
        {
            SceneChangeManager.Instance.deathToGame_SceneName = "ChaseScene";
            jumpscareCamera = FindGameObjectByName("JumpscareCamera");
            trapGage = FindGameObjectByName("TrapSlider");
        }
        catch
        {
            print("¾ø´Ù");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Jumpscare();
            Invoke("ChangeScene", 1.25f);
        }
    }

    private void Jumpscare()
    {
        GetComponent<AudioSource>().Play();
        jumpscareCamera.SetActive(false);
        trapGage.SetActive(false);
        FindGameObjectByName("ScreenCanvas").gameObject.SetActive(false);
        GetComponent<Animator>().SetTrigger("Jumpscare");
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

    private void ChangeScene()
    {
        SceneChangeManager.Instance.DeathScene();
    }
}
