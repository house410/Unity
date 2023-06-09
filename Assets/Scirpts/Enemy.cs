using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 5f; // �ʱ� �ӵ�
    public float speedIncreaseRate = 0.1f; // �ӵ� ���� ����
    public float resetPosition = -10f; // �ʱ� ��ġ�� ������ x ��ǥ

    private float speed; // ���� �ӵ�

    public void Start()
    {
        speed = startSpeed;
    }

    public void Update()
    {
        // �����ʿ��� �������� ������ �ӵ��� �����̱�
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // ��ֹ��� ���� ������ �̵��� ��� ��ġ �ʱ�ȭ
        if (transform.position.x <= resetPosition)
        {
            ResetPosition();
        }

        // �ð��� �������� �ӵ� ����
        speed += speedIncreaseRate * Time.deltaTime;
    }

    public void ResetPosition()
    {
        // �ʱ� ��ġ�� ����
        Vector3 startPosition = new Vector3(10f, transform.position.y, transform.position.z);
        transform.position = startPosition;
    }
}
