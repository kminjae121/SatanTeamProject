using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _escPanel;

    [SerializeField] private List<GameObject> settingList = new List<GameObject>();

    [SerializeField] PlayerCam _playerCam;

    public bool _isOpen;

    private void Awake()
    {
        _isOpen = true;
        _escPanel.SetActive(false);
    }


    private void Update()
    {

        if (_isOpen == true)
            OpenEsc();
        else
            CloseEsc();
    }
    public void ContinueButton()
    {
        _escPanel.SetActive(false);

        settingList.ForEach(s => s.SetActive(false));

        _playerCam.enabled = true;
        _isOpen = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void Setting()
    {
        _escPanel.SetActive(false);
        settingList[0].SetActive(true);
    }


    private void OpenEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _escPanel.SetActive(true);
            _playerCam.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _isOpen = false;
        }
    }

    private void CloseEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _escPanel.SetActive(false);
            settingList.ForEach(s => s.SetActive(false));
            _playerCam.enabled = true;
            _isOpen = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
