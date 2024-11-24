using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using System;
using System.Diagnostics;

public class PasswordNote : MonoBehaviour, IBeginDragHandler
{
    private bool isOnEnable;

    [SerializeField]
    private GameObject onSetting;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isOnEnable = !isOnEnable;

        if(isOnEnable)
        {
            onSetting.SetActive(false);
        }
        else
        {
            onSetting.SetActive(true);
        }
    }

    public void ButtonClick()
    {
        onSetting.SetActive(false);

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\password.txt";
        print(path);
        var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate));
        writer.WriteLine("password is 505");
        writer.Close();

        ProcessStartInfo startInfo = new ProcessStartInfo(path)
        {
            UseShellExecute = true
        };
        Process.Start(startInfo);
    }
}
