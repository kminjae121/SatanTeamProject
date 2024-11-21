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

    [SerializeField]
    private float deathRadius;

    private void Awake()
    {
        foreach(Transform transform in soundObj)
        {
            soundHere.Add(transform.name, transform);
        }

        audioInput._BigSound += TargetChange;
    }

    private void Start()
    {
        //movePoint.SetDestination(soundHere["PlayerCharacter(AudioInput)"].position);
    }

    public void TargetChange(string change)
    {
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
            if (colliders.name == targetPoint.name)
            {
                isPoint = false;
                print("µµÂø");
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
            MovingTarget();
    }
}
