using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SantaPoket : MonoBehaviour, IInteractable
{
    public GameObject wall;
    public void Interact()
    {
        ChatManager.Instance.Chat(2,"�ֵ��� ���� �������������� ��¿ �� ����");
        gameObject.GetComponent<ObjectOutLine>().enabled = false;
        StartCoroutine(NextChat());
    }

    private IEnumerator NextChat()
    {
        yield return new WaitForSeconds(3f);
        wall.SetActive(false);
        ChatManager.Instance.Chat(3, "[G]�� ������ ���̵� ������ ����");
        FindAnyObjectByType<Item>().isGetPresent = true;
        Destroy(gameObject);
    }
}