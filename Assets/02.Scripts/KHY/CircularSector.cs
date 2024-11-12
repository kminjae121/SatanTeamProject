using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class CircularSector : MonoBehaviour
{
    public Transform[] target;    // ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��
    public float angleRange = 30f;
    public float radius = 3f;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    bool isCollision = false;

    [SerializeField]
    private LayerMask _whatIsObstacle;

    private Vector3 interV;

    public Volume volume;

    void Update()
    {
        for(int i =0; i <target.Length; i++)
        {
            interV = target[i].position - transform.position;

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
                    print("�þ� ����");

                    RaycastHit hit;

                    if (Physics.Raycast(transform.position, interV.normalized, out hit, Mathf.Infinity, _whatIsObstacle))
                    {
                        print("�þ� ����");
                        Dangerous(dot);
                        isCollision = false;
                        target[i]?.GetComponent<IDetectGaze>().OutOfSight();
                    }
                    else
                    {
                        print("��ֹ� ������");
                        target[i]?.GetComponent<IDetectGaze>().GazeDetection(transform);
                        isCollision = true;
                    }
                }
                else
                {
                    print("�þ� ����");
                    Dangerous(dot);
                    isCollision = false;
                    target[i]?.GetComponent<IDetectGaze>().OutOfSight();
                }

            }
            else
            {
                print("�þ� ����");
                isCollision = false;
                target[i]?.GetComponent<IDetectGaze>().OutOfSight();
            }
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

    private void Safe(float dot)
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

        // ����Ƽ �����Ϳ� ��ä���� �׷��� �޼ҵ�
        private void OnDrawGizmos()
    {
        if (interV == null) return;
        Debug.DrawRay(transform.position, interV.normalized * 1000f, Color.red);

        Handles.color = isCollision ? _red : _blue;
        // DrawSolidArc(������, ��ֺ���(��������), �׷��� ���� ����, ����, ������)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}
