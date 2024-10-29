using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CircularSector : MonoBehaviour
{
    public Transform[] target;    // ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��
    public float angleRange = 30f;
    public float radius = 3f;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    bool isCollision = false;


    void Update()
    {
        for(int i =0; i <target.Length; i++)
        {
            Vector3 interV = target[i].position - transform.position;

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
                    target[i]?.GetComponent<IDetectGaze>().GazeDetection(transform);
                    isCollision = true;
                }
                else
                {
                    print("�þ� ����");
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

    // ����Ƽ �����Ϳ� ��ä���� �׷��� �޼ҵ�
    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _red : _blue;
        // DrawSolidArc(������, ��ֺ���(��������), �׷��� ���� ����, ����, ������)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}
