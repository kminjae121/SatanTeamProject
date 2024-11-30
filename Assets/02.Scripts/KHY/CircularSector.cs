using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class CircularSector : MonoBehaviour
{
    public Transform target;    // ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��
    public float angleRange = 30f;
    public float radius = 3f;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    bool isCollision = false;

    [SerializeField]
    private LayerMask _whatIsObstacle;
    
    [SerializeField]
    private LayerMask _whatIsSnowMan;

    private Vector3 interV;

    public Volume volume;

    void Update()
    {
            interV = target.position - transform.position;

            // target�� �� ������ �Ÿ��� radius ���� �۴ٸ�
            if (interV.magnitude <= radius)
            {
                // 'Ÿ��-�� ����'�� '�� ���� ����'�� ����
                float dot = Vector3.Dot(interV.normalized, transform.forward);
                // �� ���� ��� ���� �����̹Ƿ� ���� ����� cos�� ���� ���ؼ� theta�� ����
                float theta = Mathf.Acos(dot);
                // angleRange�� ���ϱ� ���� degree�� ��ȯ
                float degree = Mathf.Rad2Deg * theta;


                // �þ߰� �Ǻ�
                if (degree <= angleRange / 2f)
                {
                    if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y + 1,transform.position.z), interV.normalized, out RaycastHit hit, Vector3.Distance(transform.position, target.position), _whatIsObstacle))
                    {
                        Dangerous(dot);
                        isCollision = false;
                        target?.GetComponent<IDetectGaze>().OutOfSight();
                    }
                    else
                    {
                        target?.GetComponent<IDetectGaze>().GazeDetection(transform);
                        isCollision = true;
                    }
                }
                else
                {
                    Dangerous(dot);
                    isCollision = false;
                    target?.GetComponent<IDetectGaze>().OutOfSight();
                }

            }
            else
            {
                isCollision = false;
                target?.GetComponent<IDetectGaze>().OutOfSight();
            }
    }

    private void Dangerous(float dot)
    {
        Vignette vignette;
        float startVignette = 0f;
        float endVignette = 0.4f;

        if(volume.profile.TryGet(out vignette))
        {
            DOTween.KillAll();
            DOTween.To(() => startVignette, vloom => vignette.intensity.value = vloom, endVignette, 2);
        }
    }

    public void Safe(float dot)
    {
        Vignette vignette;
        float startVignette = 0f;
        float endVignette = 0.4f;

        if (volume.profile.TryGet(out vignette))
        {
            DOTween.KillAll();
            DOTween.To(() => endVignette, vloom => vignette.intensity.value = vloom, startVignette, 2);
        }
    }
}
