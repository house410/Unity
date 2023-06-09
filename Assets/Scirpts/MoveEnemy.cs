using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float startSpeed = 5f; // �ʱ� �ӵ�
    public float speedIncreaseRate = 0.1f; // �ӵ� ���� ����
    public float resetPosition = -10f; // �ʱ� ��ġ�� ������ x ��ǥ
    public float verticalRange = 3f; // ���Ʒ� ������ ����
    public float verticalSpeed = 2f; // ���Ʒ� ������ �ӵ�

    private float speed; // ���� �ӵ�
    private bool movingUp = true; // ���� �����̴� ������ ����

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

        // ���Ʒ��� �����̱�
        float verticalMovement = verticalSpeed * Time.deltaTime;
        if (movingUp)
        {
            transform.Translate(Vector3.up * verticalMovement);
            if (transform.position.y >= verticalRange)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * verticalMovement);
            if (transform.position.y <= -verticalRange)
            {
                movingUp = true;
            }
        }

        // �ð��� �������� �ӵ� ����
        speed += speedIncreaseRate * Time.deltaTime;
    }

    public void ResetPosition()
    {
        // �ʱ� ��ġ�� ����
        Vector3 startPosition = new Vector3(10f, Random.Range(-verticalRange, verticalRange), transform.position.z);
        transform.position = startPosition;
    }
}
