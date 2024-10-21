using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PlayerStat")]
public class PlayerStatSO : ScriptableObject
{
    public int moveSpeed;
    public int jumpSpeed;
    public float interactionTime;
    public Vector3 moveDir;
}
