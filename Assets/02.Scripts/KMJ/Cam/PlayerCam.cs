using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public bool ishit { get; set; }

    public GameObject playerCamera;

    public int mouseReversal = 1;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    private float rotationX = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        CamSetting();
    }

    private void CamSetting()
    {
        lookSpeed = SettingManager.Instance.Sensitivity;
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX * SettingManager.Instance.mouseUDInversion, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed * SettingManager.Instance.mouseLRInversion, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.forward);
        Gizmos.color = Color.white;
    }
}
