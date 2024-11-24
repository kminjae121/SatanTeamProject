using System.Collections;
using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    private bool _isOpen;

    private bool _isOpening;

    private bool _isStop;


    private ObjectOutLine _outLine;

    private void Awake()
    {
        _isStop = false;
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
                if (Input.GetKeyDown(KeyCode.E) && _isOpen && !_isStop)
                {
                    gameObject.transform.parent.TryGetComponent(out Animator animator);
                    animator.SetBool("Close", true);

                    _isStop = true;

                    StartCoroutine(Wait2());
                }
                else if (Input.GetKeyDown(KeyCode.E) && !_isOpen && !_isStop)
                {
                    gameObject.transform.parent.TryGetComponent(out Animator animator);
                    animator.SetBool("Close", false);


                    _isStop = true;

                    StartCoroutine(Wait());
                }
            }
        }
    }

    public void Open()
    {
        gameObject.transform.parent.TryGetComponent(out Animator animator);
        print(animator);
        AudioManager.Instance.PlaySound2D("OpenDoor", 0, false, SoundType.VfX);
        animator.SetBool("Open", true);

        _isStop = true;
        StartCoroutine(Wait3());
    }

    IEnumerator Wait()
    {
        AudioManager.Instance.PlaySound2D("OpenDoor", 0, false, SoundType.VfX);
        yield return new WaitForSecondsRealtime(1.3f);

        _isStop = false;    
        _isOpen = true;
    }

    IEnumerator Wait2()
    {

        yield return new WaitForSeconds(0.31f);

        AudioManager.Instance.PlaySound2D("CloseDoor", 0, false, SoundType.VfX);

        yield return new WaitForSecondsRealtime(1.3f);

        _isStop = false;
        _isOpen = false;
    }

    IEnumerator Wait3()
    {
        yield return new WaitForSeconds(1.3f);
         
        _isOpening = true;
        _isStop = false;
        _isOpen = true;
    }


}
