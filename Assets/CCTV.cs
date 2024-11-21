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

    public Transform target;    // ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��
    public float angleRange = 30f;
    public float radius = 3f;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    bool isCollision = false;

    [SerializeField]
    private LayerMask _whatIsSnowMan;

    private Vector3 interV;

    RaycastHit hit;


    private void Update()
    {
        print(Mathf.Abs(transform.rotation.y));
        if (Mathf.Abs(transform.rotation.y) < 0.8f)
        {
            rotateSpeed *= -1;
            print("�Ѿ");
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
                print("�þ� ����");
                Dangerous();
                isCollision = true;
            }
            else
            {
                print("�þ� ����");
                isCollision = false;
            }
        }
    }

    private void Dangerous()
    {
        print("cctv�� ������");
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