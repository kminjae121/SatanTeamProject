using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{

    public void EndingScene()
    {
        Invoke("Change", 4);
    }

    private void Change()
    {
        SceneManager.LoadScene("ChaseScene");
    }
}
