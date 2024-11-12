using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class CircularSector : MonoBehaviour
{
    public Transform[] target;    // 부채꼴에 포함되는지 판별할 타겟
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

            // target과 나 사이의 거리가 radius 보다 작다면
            if (interV.magnitude <= radius)
            {
                // '타겟-나 벡터'와 '내 정면 벡터'를 내적
                float dot = Vector3.Dot(interV.normalized, transform.forward);
                // 두 벡터 모두 단위 벡터이므로 내적 결과에 cos의 역을 취해서 theta를 구함
                float theta = Mathf.Acos(dot);
                // angleRange와 비교하기 위해 degree로 변환
                float degree = Mathf.Rad2Deg * theta;
                

                // 시야각 판별
                if (degree <= angleRange / 2f)
                {
                    print("시야 들어옴");

                    RaycastHit hit;

                    if (Physics.Raycast(transform.position, interV.normalized, out hit, Mathf.Infinity, _whatIsObstacle))
                    {
                        print("시야 나감");
                        Dangerous(dot);
                        isCollision = false;
                        target[i]?.GetComponent<IDetectGaze>().OutOfSight();
                    }
                    else
                    {
                        print("장애물 감지됨");
                        target[i]?.GetComponent<IDetectGaze>().GazeDetection(transform);
                        isCollision = true;
                    }
                }
                else
                {
                    print("시야 나감");
                    Dangerous(dot);
                    isCollision = false;
                    target[i]?.GetComponent<IDetectGaze>().OutOfSight();
                }

            }
            else
            {
                print("시야 나감");
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

        // 유니티 에디터에 부채꼴을 그려줄 메소드
        private void OnDrawGizmos()
    {
        if (interV == null) return;
        Debug.DrawRay(transform.position, interV.normalized * 1000f, Color.red);

        Handles.color = isCollision ? _red : _blue;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}
