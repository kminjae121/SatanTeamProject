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

    [SerializeField]
    private Animator animator;

    private Transform targetPoint;
    private bool isPoint;
    public bool isPlayerFollow;

    [SerializeField]
    private float foundRadius;
    [SerializeField]
    private float deadRadius;

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
            animator.SetFloat("Velocity", 0.2f);
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
        Collider[] collider = Physics.OverlapSphere(transform.position, foundRadius);

        animator.SetFloat("Velocity", 0.5f);

        foreach (Collider colliders in collider)
        {
            if(colliders.name == "PlayerCharacter(AudioInput)")
            {
                movePoint.SetDestination(soundHere["PlayerCharacter(AudioInput)"].position);
                isPlayerFollow = true;
                print("플레이어 찾음");
                animator.SetFloat("Velocity", 1f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, foundRadius);
        Gizmos.color = Color.white;
    }

    private void Update()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, deadRadius);

        foreach (Collider colliders in collider)
        {
            if (colliders.name == "PlayerCharacter(AudioInput)")
            {
                print("뒤@짐");
            }
        }

        if (movePoint.velocity.sqrMagnitude >= 0.2f * 0.2f && movePoint.remainingDistance <=0.5f)
        {
            isPoint = false;
            animator.SetFloat("Velocity", 0.2f);
        }

        if (isPoint)
        {
            MovingTarget();
        }
    }
}
