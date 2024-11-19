using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LockKey1"))
        {
            gameObject.TryGetComponent(out Animator animator);

            animator.SetBool("Open", true);
        }
        else
            return;
    }
}
