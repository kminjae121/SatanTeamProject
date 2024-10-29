using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    [SerializeField] private Light lightObj;
    [SerializeField] private float randomMiniumum = 0;
    [SerializeField] private float randomMaximum = 1;

    public bool active = true;

    private void Start()
    {
        lightObj = GetComponent<Light>();
        StartCoroutine(Blinking());
    }

    private IEnumerator Blinking()
    {
        while (active)
        {
            for (int i = 0; i < Random.Range(3, 6); i ++)
            {
                lightObj.enabled = false;
                yield return new WaitForSecondsRealtime(Random.Range(randomMiniumum, randomMaximum));
                lightObj.enabled = true;
                yield return new WaitForSecondsRealtime(Random.Range(randomMiniumum, randomMaximum));
            }
            lightObj.enabled = false;
            yield return new WaitForSecondsRealtime(Random.Range(randomMiniumum + 1, randomMaximum + 1));
        }
    }
}
