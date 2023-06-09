using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public Transform groundObject; // �� ��ü�� Transform ������Ʈ�� �����մϴ�.
    public float gravity = 20f; // �߷��� ������ �����մϴ�.
    public float jumpForce = 5f;
    private Vector3 velocity = Vector3.zero; // ���� ���� �ӵ��� �����մϴ�.
    private bool isGrounded = false;

    public void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        // �÷��̾��� AABB �ּ� ���� �ִ� ���� ���մϴ�.
        Vector3 playerMin = transform.position - transform.localScale / 2;
        Vector3 playerMax = transform.position + transform.localScale / 2;

        // �� ��ü�� AABB �ּ� ���� �ִ� ���� ���մϴ�.
        Vector3 groundMin = groundObject.position - groundObject.localScale / 2;
        Vector3 groundMax = groundObject.position + groundObject.localScale / 2;

        // �浹 ���θ� �ʱ�ȭ�մϴ�.
        bool collision = true;

        // X �� �������� üũ�մϴ�.
        if (playerMin.x > groundMax.x || playerMax.x < groundMin.x)
        {
            collision = false;
        }

        // Y �� �������� üũ�մϴ�.
        if (playerMin.y > groundMax.y || playerMax.y < groundMin.y)
        {
            collision = false;
        }

        // Z �� �������� üũ�մϴ�.
        if (playerMin.z > groundMax.z || playerMax.z < groundMin.z)
        {
            collision = false;
        }

        // �� ��ü ���� �÷��̾ ���� �� ó���մϴ�.
        if (collision)
        {
            // �÷��̾��� Y ��ǥ�� �� ��ü�� �ִ� Y ��ǥ�� �����մϴ�.
            float newY = groundMax.y + transform.localScale.y / 2;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // ���� ���� �ӵ��� 0���� �ʱ�ȭ�մϴ�.
            velocity.y = 0f;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // �÷��̾ ���� ������ŵ�ϴ�.
            velocity.y = jumpForce;
        }
    }
}

