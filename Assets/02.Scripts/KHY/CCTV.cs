using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    private bool isRotateClaim;

    [SerializeField]
    private float rotateSpeed = 5f;

    public Transform target;    // ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��
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

    private bool _isStop;

    private void Awake()
    {
        audioInput = FindAnyObjectByType<AudioInput>();
        target = FindAnyObjectByType<Player>().transform;
    }

    private void Update()
    {
        if (Mathf.Abs(transform.rotation.y) < 0.8f)
        {
            rotateSpeed *= -1;
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        }
        else
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));

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
                if(!isTutorial)
                {
                    Dangerous();
                }
                
                if(_isStop == true)
                {
                    _isStop = false;
                    AudioManager.Instance.PlaySound2D("Alarm", 0, true, SoundType.SFX);
                }
                isCollision = true;
            }
            else
            {
                AudioManager.Instance.StopLoopSound("Alarm");
                isCollision = false;
                _isStop = true;
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
}
