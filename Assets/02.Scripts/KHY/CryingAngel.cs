using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CryingAngel : MonoBehaviour, IDetectGaze
{
    [SerializeField]
    private NavMeshAgent movePoint;

    [SerializeField]
    private Transform player;

    public bool isStop;

    [Header("감지범위")]
    [SerializeField]
    private float deathRadius;

    [SerializeField]
    private LayerMask _whatIsPlayer;

    [SerializeField]
    private GameObject deathObj;

    private AsyncOperation asyncOperation;

    private void Awake()
    {
        movePoint = GetComponent<NavMeshAgent>();
        //movePoint.SetDestination(player.position);
    }

    public void Start()
    {
        movePoint.SetDestination(player.position);
        asyncOperation = SceneManager.LoadSceneAsync("DeathScene");
        asyncOperation.allowSceneActivation = false;
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

        Collider[] collider = Physics.OverlapSphere(transform.position,deathRadius,_whatIsPlayer);

        foreach(Collider colliders in collider)
        {
            if (colliders.tag == "Player")
            {
                deathObj.SetActive(true);
                StartCoroutine(DeathScene());
                //Destroy(gameObject);
            }
        }
    }

    private IEnumerator DeathScene()
    {
        yield return new WaitForSecondsRealtime(1.4f);
        asyncOperation.allowSceneActivation = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, deathRadius);
        Gizmos.color = Color.white;
    }
}
