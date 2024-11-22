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
    public bool isPlayerFollow;

    [SerializeField]
    private float deathRadius;

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

    public void AddTransform(Transform transform)
    {
        soundHere.Add(transform.name, transform);
    }

    private void LessPlayer()
    {
        if(isPlayerFollow)
        {
            movePoint.isStopped = true;
            isPlayerFollow = false;
            print("플레이어 놓침");
        }
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
