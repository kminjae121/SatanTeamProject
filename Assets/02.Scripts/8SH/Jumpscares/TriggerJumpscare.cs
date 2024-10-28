using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerJumpscare : MonoBehaviour
{
    public abstract void OnTriggerEnter(Collider other);
    public abstract void Active();
    public abstract void CheckActiveable();
}
