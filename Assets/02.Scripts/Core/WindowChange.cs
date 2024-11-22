using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowChange : MonoBehaviour
{
    private RawImage rawImage;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Start()
    {
        print("焼たたたたたたたたたたたたたたたたたすぉ沌えぉ");
        rawImage.texture = BGManager.Instance.GetPCWallpaper();
    }
}
