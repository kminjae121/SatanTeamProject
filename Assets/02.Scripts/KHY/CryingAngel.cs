using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CryingAngel : MonoBehaviour, IDetectGaze
{
    [SerializeField]
    private NavMeshAgent movePoint;

    [SerializeField]
    private Transform player;

    public bool isStop;

    private void Awake()
    {
        movePoint = GetComponent<NavMeshAgent>();
        //movePoint.SetDestination(player.position);
    }

    public void Start()
    {
        movePoint.SetDestination(player.position);
    }

    public void GazeDetection(Transform player)
    {
        movePoint.isStopped = true;
        isStop = true;
    }

    private void Update()
    {
        //    if(!isStop)
        //    {
        //        //Vector3 interV = player.position - transform.position;
        //        //rigidbody.velocity = interV * 1f;
        //    }
        //    else
        //    {
        //        rigidbody.velocity = Vector3.zero;
        //    }
    }

    public void OutOfSight()
    {
        movePoint.SetDestination(player.position);
        movePoint.isStopped = false;
        isStop = false;
    }
}
