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
    [SerializeField]
    private GameObject volume;

    private CircularSector circularSector;

    public void ActiveMap()
    {
        stageMap.SetActive(true);
    }

    public void Setting()
    {
        circularSector = player.GetComponentInChildren<CircularSector>();
        circularSector.target = null;
        circularSector.Safe(8282f);
        circularSector.enabled = false;
        Destroy(snowman);
    }

    public void MonsterSpawn()
    {
        soundMonseter.SetActive(true);
    }
}
