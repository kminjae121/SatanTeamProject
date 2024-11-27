using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WindowChange : MonoBehaviour
{
    private RawImage rawImage;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Start()
    {
        try
        {
            rawImage.texture = BGManager.Instance.GetPCWallpaper();
        }
        catch(Exception e)
        {
            print("�� �ҷ����� ���ȭ����;;");
            print($"Error: {e.Message}");
        }
    }
}
