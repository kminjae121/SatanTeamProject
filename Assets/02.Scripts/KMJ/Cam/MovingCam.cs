using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingCam : MonoBehaviour
{
    public Sequence _movesequence;

    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _movesequence = DOTween.Sequence()
            
            .SetLoops(-1)
            .SetAutoKill(false);

        Debug.Log("¿Í¿ì");
    }


    private void Update()
    {
        if (_player.isMoving)
            _movesequence.Play();
        else
            _movesequence.Pause();
    }
}
