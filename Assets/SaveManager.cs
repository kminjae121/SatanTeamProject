using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    public Transform CurrentPlayerTrm { get; private set; }
    public bool isAlreadyStart;

    public void PlayerSpawnPoint()
    {
        Transform currentTrm = FindAnyObjectByType<Player>().GetComponent<Transform>();
        isAlreadyStart = true;
        CurrentPlayerTrm = currentTrm;
    }
}
