using UnityEngine;
using UnityEngine.Events;

public class CloseTrigger : MonoBehaviour
{
    public UnityEvent action;

    private void OnTriggerEnter(Collider other)
    {
        action?.Invoke();
    }
}
