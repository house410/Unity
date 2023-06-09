using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public Transform groundObject; // 땅 객체의 Transform 컴포넌트를 참조합니다.
    public float gravity = 20f; // 중력의 강도를 설정합니다.
    public float jumpForce = 5f;
    private Vector3 velocity = Vector3.zero; // 수직 방향 속도를 저장합니다.
    private bool isGrounded = false;

    public void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        // 플레이어의 AABB 최소 값과 최대 값을 구합니다.
        Vector3 playerMin = transform.position - transform.localScale / 2;
        Vector3 playerMax = transform.position + transform.localScale / 2;

        // 땅 객체의 AABB 최소 값과 최대 값을 구합니다.
        Vector3 groundMin = groundObject.position - groundObject.localScale / 2;
        Vector3 groundMax = groundObject.position + groundObject.localScale / 2;

        // 충돌 여부를 초기화합니다.
        bool collision = true;

        // X 축 기준으로 체크합니다.
        if (playerMin.x > groundMax.x || playerMax.x < groundMin.x)
        {
            collision = false;
        }

        // Y 축 기준으로 체크합니다.
        if (playerMin.y > groundMax.y || playerMax.y < groundMin.y)
        {
            collision = false;
        }

        // Z 축 기준으로 체크합니다.
        if (playerMin.z > groundMax.z || playerMax.z < groundMin.z)
        {
            collision = false;
        }

        // 땅 객체 위에 플레이어가 있을 때 처리합니다.
        if (collision)
        {
            // 플레이어의 Y 좌표를 땅 객체의 최대 Y 좌표로 조정합니다.
            float newY = groundMax.y + transform.localScale.y / 2;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // 수직 방향 속도를 0으로 초기화합니다.
            velocity.y = 0f;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // 플레이어를 위로 점프시킵니다.
            velocity.y = jumpForce;
        }
    }
}

