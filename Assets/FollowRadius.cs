using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRadius : MonoBehaviour
{
    public event Action OnLessPlayer;

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "PlayerCharacter(AudioInput)")
        {
            OnLessPlayer?.Invoke();
        }
    }
}
