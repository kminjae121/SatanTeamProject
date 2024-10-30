using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsObject;
    public bool ishit { get; set; }

    public GameObject playerCamera;

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

        Debug.Log(ishit);
    }

    private void CamSetting()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.forward);
        Gizmos.color = Color.white;
    }
}
