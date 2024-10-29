using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectGaze
{
    public void GazeDetection(Transform player);

    public void OutOfSight();
}
