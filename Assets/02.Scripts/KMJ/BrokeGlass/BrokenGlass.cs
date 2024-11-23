using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BrokenGlass : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3f);

        gameObject.TryGetComponent(out MeshRenderer mesh);

        mesh.material.DOFade(0, 3f);

        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }
}
