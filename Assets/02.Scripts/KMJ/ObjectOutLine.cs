using UnityEngine;

public class ObjectOutLine : MonoBehaviour
{
    private int isOutLineHesh = Shader.PropertyToID("_IsOutLine");
    private Material[] _mat;

    private LookAtItem _lookAtCompo;
   [field : SerializeField] public bool _isOutLine { get; set; }

    private void Awake()
    {
        _lookAtCompo = GameObject.Find("PlayerCharacter(AudioInput)").GetComponent<LookAtItem>();
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
        {
            _mat[1].SetInt(isOutLineHesh, 1);
        }
        else
            _mat[1].SetInt(isOutLineHesh, 0);
    }


}
