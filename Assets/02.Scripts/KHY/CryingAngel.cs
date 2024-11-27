using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CryingAngel : MonoBehaviour, IDetectGaze
{
    [SerializeField]
    private NavMeshAgent movePoint;

    public Transform player;

    public CircularSector circularSector;

    public bool isStop;

    [Header("감지범위")]
    [SerializeField]
    private float deathRadius;

    [SerializeField]
    private LayerMask _whatIsPlayer;

    private bool isPlay;

    private void Awake()
    {
        movePoint = GetComponent<NavMeshAgent>();
        //movePoint.SetDestination(player.position);
        circularSector.enabled = true;
    }

    public void Start()
    {
        movePoint.SetDestination(player.position);
    }

    public void GazeDetection(Transform player)
    {
        movePoint.isStopped = true;
        isStop = true;
        if (!isPlay)
            AudioManager.Instance.PlaySound2D("ComeOn", 0, true, SoundType.VfX);
        isPlay = true;
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
        AudioManager.Instance.StopLoopSound("ComeOn");
        isPlay = false;
        //AudioManager.Instance.StopLoopSound("ComeOn");
        movePoint.SetDestination(player.position);
        movePoint.isStopped = false;
        isStop = false;
        Collider[] collider = Physics.OverlapSphere(transform.position,deathRadius,_whatIsPlayer);

        foreach(Collider colliders in collider)
        {
            if (colliders.tag == "Player")
            {
                player.GetComponent<Player>().deathObj.SetActive(true);
                AudioManager.Instance.PlaySound2D("Scary", 0, false, SoundType.VfX);
                StartCoroutine(DeathScene());
                //Destroy(gameObject);
            }
        }
    }

    private IEnumerator DeathScene()
    {
        yield return new WaitForSecondsRealtime(1.4f);
        SceneChangeManager.Instance.DeathScene();
        print("안되면 억까");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, deathRadius);
        Gizmos.color = Color.white;
    }
}
