using UnityEngine;

public class ObjectOutLine : MonoBehaviour
{
    private int isOutLineHesh = Shader.PropertyToID("_IsOutLine");
    private Material[] _mat;

    public bool _isOutLine { get; set; }

    private void Awake()
    {
        _isOutLine = false;
        _mat = GetComponent<MeshRenderer>().materials;       
    }

    private void Update()
    {
        Test();
    }

    public void Test()
    {
        if (_isOutLine)
            _mat[1].SetInt(isOutLineHesh, 1);
        else
            _mat[1].SetInt(isOutLineHesh, 0);
    }


}
