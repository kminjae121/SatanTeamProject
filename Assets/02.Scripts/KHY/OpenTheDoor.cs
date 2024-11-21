using System.Collections;
using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    private static int _keyNumber = 1;

    private bool _isOpen;

    private bool _isOpening;


    private ObjectOutLine _outLine;

    private void Awake()
    {
        _outLine = GetComponent<ObjectOutLine>();
        _isOpening = false;
    }

    private void Update()
    {
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (_isOpening)
        {
            if (_outLine._isOutLine)
            {
                if (Input.GetKeyDown(KeyCode.E) && _isOpen)
                {
                    gameObject.transform.parent.TryGetComponent(out Animator animator);

                    animator.SetBool("Close", true);

                    StartCoroutine(Wait2());
                }
                else if (Input.GetKeyDown(KeyCode.E) && !_isOpen)
                {
                    gameObject.transform.parent.TryGetComponent(out Animator animator);

                    animator.SetBool("Close", false);

                    StartCoroutine(Wait());
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag($"LockKey{_keyNumber}"))
        {
            gameObject.transform.parent.TryGetComponent(out Animator animator);

            animator.SetBool("Open", true);

            _keyNumber++;

            _isOpening = true;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1.3f);

        _isOpen = true;
    }

    IEnumerator Wait2()
    {
        yield return new WaitForSecondsRealtime(1.3f);

        _isOpen = false;
    }


}
