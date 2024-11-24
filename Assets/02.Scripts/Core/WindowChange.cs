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
            print("못 불러오는 배경화면임;;");
            print($"Error: {e.Message}");
        }
    }
}
