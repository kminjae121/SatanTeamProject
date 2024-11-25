using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class ChatManager : MonoBehaviour
{
    public static ChatManager Instance;

    [SerializeField]
    private string[] chatList;

    private int currentTextIdx = 0;

    [SerializeField]
    private TextMeshProUGUI chatText;

    private Coroutine coroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Chat(int frontTime,string directText = "null")
    {
        if (coroutine != null)
        {
            chatText.DOKill();
            StopCoroutine(coroutine);
            coroutine = null;
        }
        try
        {
            if(directText == "null")
            {
                print("焼たたたたたたたたたたたたたたたたたたたたたたたたたたたたたたたけぁい反ばったでいお焼でばっぬいせしそ虞ばで っそせしず妃と っぬせぉしそい鋳硲 たっぬけぉしい鋳たっで ぬ");
                chatText.text = chatList[currentTextIdx];
                print(chatList[currentTextIdx]);
                currentTextIdx++;
            }
            else
            {
                chatText.text = directText;
            }
        }
        catch(IndexOutOfRangeException e)
        {
            Console.WriteLine("希 句随 企紫亜 蒸柔艦陥!");
            Console.WriteLine($"Error: {e.Message}");
        }
        finally
        {
            print(chatList[currentTextIdx]);
        }
        coroutine = StartCoroutine(TextRoutine(frontTime));
    }

    private IEnumerator TextRoutine(int frontTime)
    {
        chatText.DOFade(1, 1);
        yield return new WaitForSeconds(frontTime);
        chatText.DOFade(0, 1);
    }
}
