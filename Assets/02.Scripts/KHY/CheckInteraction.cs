using TMPro;
using UnityEngine;

public class CheckInteraction : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private float _checkRate = 0.05f;
    [SerializeField] private float _maxDistance = 3.0f;
    [SerializeField] private LayerMask _layerMask;
    #endregion

    #region private field
    private float _lastCheckTime;

    private GameObject _curGameobject;
    private IInteractable _curInteractable;

    public GameObject OutLineObj = null;

    private Camera _camera;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;

        _layerMask = LayerMask.GetMask("Interactable");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _lastCheckTime > _checkRate)
        {
            _lastCheckTime = Time.time;

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _maxDistance, _layerMask))
            {
                print("쉽지않음");
                if (hit.collider.gameObject != _curGameobject)
                {
                    _curGameobject = hit.collider.gameObject;
                    _curInteractable = hit.collider.GetComponent<IInteractable>();
                }

                OutLineObj = hit.transform.gameObject;

                OutLineObj.transform.TryGetComponent(out ObjectOutLine outLine);

                outLine._isOutLine = true;
            }
            else
            {
                _curGameobject = null;
                _curInteractable = null;
            }
        }
    }
    public void OnInteraction()
    {
        print("클릭 들어옴");
        if (_curInteractable != null)
        {
            _curInteractable.Interact();
            //_curGameobject = null;
            //_curInteractable = null;
        }
    }

}

