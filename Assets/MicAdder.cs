using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicAdder : MonoBehaviour
{
    [SerializeField]
    private Michsky.UI.Dark.HorizontalSelector horizontalSelector;

    private void Start()
    {
        print("DDDDDDDDDDd");
        print(horizontalSelector.itemList.Count);
    }

}
