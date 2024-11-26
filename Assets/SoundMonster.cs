using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
        audioInput = FindAnyObjectByType<AudioInput>();
        soundObj[0] = FindAnyObjectByType<Player>().transform;
        foreach(Transform transform in soundObj)
        {
            soundHere.Add(transform.name, transform);
        }
        followRadiusCollider = GetComponentInChildren<FollowRadius>();

        audioInput._BigSound += TargetChange;
        followRadiusCollider.OnLessPlayer += LessPlayer;
        FindAnyObjectByType<Item>().soundMonster = this;
    }

    public void AddTransform(Transform transform)
    {
        try
        {
        soundHere.Add(transform.name, transform);
        }
        catch(Exception e)
        {
            soundHere.Remove(transform.name);
            soundHere.Add(transform.name, transform);
        }
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
                audioInput.GetComponentInParent<Player>().soundMonsterDeathObj.SetActive(true);
                StartCoroutine(DeathScene());
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

    private IEnumerator DeathScene()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneChangeManager.Instance.DeathScene();
    }
}
