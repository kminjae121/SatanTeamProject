using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PassWorldDoor : MonoBehaviour,IUseItem
{
    [SerializeField] private InputField _passWorld;

    public void Use()
    {
       
    }

    private void Update()
    {
        WhatIsRightPassWorld();
        Use();
    }

    private void WhatIsRightPassWorld()
    {
        if(_passWorld.text == "1630")
        {
            _passWorld.transform.gameObject.SetActive(false);

            gameObject.TryGetComponent(out Animator animator);

            animator.SetBool("Open", true);
        }
    }
}
