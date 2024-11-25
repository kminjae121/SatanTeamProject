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

    public void Setting()
    {
        stageMap.SetActive(true);
        player.GetComponent<CircularSector>().enabled = false;
        Destroy(snowman);
    }

    public void MonsterSpawn()
    {
        soundMonseter.SetActive(true);
    }
}
