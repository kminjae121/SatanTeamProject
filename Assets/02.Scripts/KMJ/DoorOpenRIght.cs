using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DoorOpenRIght : MonoBehaviour
{
    public UnityEvent doorEvent;
    private bool enable = true;

    private ObjectOutLine _outLine;

    private void Awake()
    {
        _outLine = GetComponent<ObjectOutLine>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && enable && _outLine._isOutLine)
        {
            enable = false;
            doorEvent?.Invoke();
        }
    }
}
