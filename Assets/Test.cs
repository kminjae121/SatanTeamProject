using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    [SerializeField]
    private LayerMask _whatIsBase;
    private NavMeshAgent _navAgent;
    private Camera _mainCam;
    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _mainCam = Camera.main; //메인 카메라 캐싱
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos;
            if (GetMouseWorldPosition(out pos))
            {
                _navAgent.destination = pos;
            }
        }
    }

    public bool GetMouseWorldPosition(out Vector3 pos)
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool result = Physics.Raycast(ray, out hit, _mainCam.farClipPlane, _whatIsBase);
        if (result)
        {
            pos = hit.point;
            return true;
        }
        pos = Vector3.zero;
        return false;
    }
}