using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoundMonster : MonoBehaviour
{
    [SerializeField]
    private Transform[] soundObj;

    private Dictionary<string, Transform> soundHere = new Dictionary<string, Transform>();

    [SerializeField]
    private NavMeshAgent movePoint;

    [SerializeField]
    private AudioInput audioInput;


    private Transform targetPoint;
    private bool isPoint;
    private bool isPlayerFollow;

    [SerializeField]
    private float deathRadius;

    [SerializeField]
    private LayerMask whatIsPlayer;

    private FollowRadius followRadiusCollider;

    private void Awake()
    {
        foreach(Transform transform in soundObj)
        {
            soundHere.Add(transform.name, transform);
        }
        followRadiusCollider = GetComponentInChildren<FollowRadius>();

        audioInput._BigSound += TargetChange;
        followRadiusCollider.OnLessPlayer += LessPlayer;
    }

    private void LessPlayer()
    {
        movePoint.isStopped = true;
        isPlayerFollow = false;
        print("플레이어 놓침");
    }

    public void TargetChange(string change)
    {
        if (isPlayerFollow) return;
        movePoint.isStopped = false;
        movePoint.SetDestination(soundHere[change].position);
        targetPoint = soundHere[change];
        isPoint = true;
    }

    private void MovingTarget()
    {
        print(targetPoint.name);
        Collider[] collider = Physics.OverlapSphere(transform.position, deathRadius);

        foreach (Collider colliders in collider)
        {
            if(colliders.name == "PlayerCharacter(AudioInput)")
            {
                movePoint.SetDestination(soundHere["PlayerCharacter(AudioInput)"].position);
                isPlayerFollow = true;
                print("플레이어 찾음");
            }
            else if (colliders.name == targetPoint.name)
            {
                isPoint = false;
                print("도착");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, deathRadius);
        Gizmos.color = Color.white;
    }

    private void Update()
    {
        if (isPoint)
        {
            MovingTarget();
        }
    }
}
