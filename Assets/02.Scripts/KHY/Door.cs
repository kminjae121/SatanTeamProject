using UnityEngine;
using System.Collections;

// �� Ŭ������ ��ȣ �ۿ� ������ �������̽��� ����
public class Door : MonoBehaviour, IInteractable
{
    #region private field
    //[SerializeField] private bool _isLocked;
    //[SerializeField] private bool _isOpened;
    //[SerializeField] private float OpenCloseTime;
    #endregion

    #region public field
    public bool _isLocked { get; private set; }
    public bool _isOpened { get; private set; }
    public float OpenCloseTime { get; private set; }

    public Vector3 OpenRotation { get; private set; }
    public Vector3 closeRotation { get; private set; }
    #endregion

    private void Awake()
    {
        _isLocked = false;
        _isOpened = false;
        OpenCloseTime = 1.0f;
        OpenRotation = new Vector3(0, 90, 0);
        closeRotation = new Vector3(0, 0, 0);
    }

    public string GetInteractText()
    {
        if (_isLocked)
            return "���� ����ֽ��ϴ�. ���谡 �־���մϴ�.";  // ������ �õ������� �ʾҴµ� ����ִٰ� ��
        else if (_isOpened)
            return "[G] �� �ݱ�";
        else
            return "[G] �� ����";
    }

    // ��ȣ �ۿ� �������̽��� �޼��� ����
    public void Interact()
    {
        if (_isOpened)
        {
            StartCoroutine(RotateDoor(closeRotation));
        }
        else if (!_isLocked && !_isOpened)
        {
            StartCoroutine(RotateDoor(OpenRotation));
        }
    }

    private IEnumerator RotateDoor(Vector3 targetRotation)
    {
        float elapsedTime = 0;

        Quaternion startRotation = transform.rotation;
        Quaternion targetQuaternion = Quaternion.Euler(targetRotation);

        while (elapsedTime < OpenCloseTime)
        {
            transform.parent.rotation = Quaternion.Slerp(startRotation, targetQuaternion, (elapsedTime / OpenCloseTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _isOpened = !_isOpened;
    }

    // ���ʹ� �� �յڷ� Ʈ���� �浹 ������ �����Ͽ� �ڵ����� �������� ó����
    private void OnTriggerEnter(Collider other)
    {
        if (!_isLocked && !_isOpened && other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            _isOpened = true;
            StartCoroutine(RotateDoor(OpenRotation));
        }
    }
}

