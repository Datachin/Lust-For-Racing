using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target; // ��������� �� ��'���, �� ���� ������ �� ���������
    public float smoothSpeed = 5f; // �������� ���� ������
    private Vector3 offset; // ³������ �� ������� � �������

    void Start()
    {
        // ����������� ������� �� ������� � ������� (���������� ����)
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // ����������� ������� ��������� ������, � ����������� ��������
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // ������������ ���� ��������� ������
        transform.position = smoothedPosition;
    }
}
