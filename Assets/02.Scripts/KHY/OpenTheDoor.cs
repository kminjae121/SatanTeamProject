using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    private static int _keyNumber;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag($"LockKey{_keyNumber}"))
        {
            gameObject.TryGetComponent(out Animator animator);

            animator.SetBool("Open", true);

            _keyNumber++;
        }
        else
            return;
    }
}
