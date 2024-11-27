using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    public string chat;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ChatManager.Instance.Chat(3, chat);
            Destroy(gameObject);
        }
    }
}
