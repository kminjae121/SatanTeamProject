using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private PlayerStatSO playerStatSO;
    public int moveSpeed { get; set; }
    public int jumpSpeed { get; set; }
    public float interactionTime { get; set; }
    public Vector3 moveDir;

    private void Awake()
    {
        moveSpeed = playerStatSO.moveSpeed;
        jumpSpeed = playerStatSO.jumpSpeed;
        interactionTime = playerStatSO.interactionTime;
        moveDir = playerStatSO.moveDir;
    }
}
