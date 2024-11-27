using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Setting : MonoBehaviour
{
    [SerializeField]
    private GameObject stageMap;
    [SerializeField]
    private GameObject snowman;
    [SerializeField]
    private GameObject soundMonseter;
    [SerializeField]
    private Player player;

    private CircularSector circularSector;

    public void Setting()
    {
        stageMap.SetActive(true);
        circularSector = player.GetComponentInChildren<CircularSector>();
        circularSector.target = null;
        circularSector.enabled = false;
        Destroy(snowman);
    }

    public void MonsterSpawn()
    {
        soundMonseter.SetActive(true);
    }
}
