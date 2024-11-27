using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosseKillTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            SceneChangeManager.Instance.DeathScene();
        }
    }
}
