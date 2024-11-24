using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    private bool isRotateClaim;

    [SerializeField]
    private float rotateSpeed = 5f;

    public Transform target;    // 부채꼴에 포함되는지 판별할 타겟
    public float angleRange = 30f;
    public float radius = 3f;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    bool isCollision = false;

    [SerializeField]
    private LayerMask _whatIsSnowMan;

    private AudioInput audioInput;

    private Vector3 interV;

    RaycastHit hit;

    public bool isTutorial;

    [SerializeField]
    private GameObject warningLight;

    private Coroutine coroutine;

    private void Awake()
    {
        audioInput = FindAnyObjectByType<AudioInput>();
    }

    private void Update()
    {
        if (Mathf.Abs(transform.rotation.y) < 0.8f)
        {
            rotateSpeed *= -1;
            print("넘어감");
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        }
        else
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));

        interV = target.position - transform.position;

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
                if(!isTutorial)
                    Dangerous();
                isCollision = true;
                AudioManager.Instance.PlaySound2D("Alarm", 0, true, SoundType.VfX);
            }
            else
            {
                AudioManager.Instance.StopLoopSound("Alarm");
                print("시야 나감");
                isCollision = false;
            }
        }
    }

    private void Dangerous()
    {
        audioInput._BigSound?.Invoke("PlayerCharacter(AudioInput)");
        Warning();
    }

    private void Warning()
    {
        if(coroutine == null)
            coroutine = StartCoroutine(WarningRoutine());
    }

    private IEnumerator WarningRoutine()
    {
        warningLight.SetActive(false);
        yield return new WaitForSeconds(1f);
        warningLight.SetActive(true);
        yield return new WaitForSeconds(1f);
        coroutine = null;
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
