using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventParam : MonoBehaviour
{
    public Tape tape;
    public TelevisionObj television;

    public void VideoChange()
    {
        television.ChangeVideo(tape);
    }
}
