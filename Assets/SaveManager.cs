using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public bool isAlreadyStart;

    public static SaveManager Instance;

    public bool isFirstSpawn;
    public bool isSecondSpawn;
    public bool isThirdSpawn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void PlayerSpawnPoint()
    {
        if (isSecondSpawn)
            isThirdSpawn = true;
        else if (isFirstSpawn)
            isSecondSpawn = true;
        else if (isAlreadyStart)
            isFirstSpawn = true;
        else
            isAlreadyStart = true;
    }
}
