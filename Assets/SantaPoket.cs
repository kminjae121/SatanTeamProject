using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SantaPoket : MonoBehaviour, IInteractable
{
    public UnityEvent unityEvent;

    public void Interact()
    {
        ChatManager.Instance.Chat(2,"�ֵ��� ���� �������������� ��¿ �� ����");
        StartCoroutine(NextChat());
    }

    private IEnumerator NextChat()
    {
        yield return new WaitForSeconds(3f);
        unityEvent?.Invoke();
        ChatManager.Instance.Chat(3, "[G]�� ������ ���̵� ������ ����");
        FindAnyObjectByType<Item>().isGetPresent = true;
        Destroy(gameObject);
    }
}
