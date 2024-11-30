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
            AudioManager.Instance.PlaySound2D("ComeOn", 0, true, SoundType.SFX);
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
                FindObjectWithComponent<Light>().enabled = false;
                player.GetComponent<Player>().deathObj.GetComponentInChildren<Light>().enabled = true;
                AudioManager.Instance.PlaySound2D("Scary", 0, false, SoundType.SFX);
                StartCoroutine(DeathScene());
                //Destroy(gameObject);
            }
        }
    }

    public T FindObjectWithComponent<T>() where T : Component
    {
        T[] allComponents = Resources.FindObjectsOfTypeAll<T>();
        foreach (T component in allComponents)
        {
            if (component.hideFlags == HideFlags.None)
            {
                return component.GetComponent<T>();
            }
        }
        return null;
    }

    private IEnumerator DeathScene()
    {
        yield return new WaitForSecondsRealtime(1.4f);
        SceneChangeManager.Instance.DeathScene();
    }
}
