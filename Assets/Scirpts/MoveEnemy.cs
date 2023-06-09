using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float startSpeed = 5f; // 초기 속도
    public float speedIncreaseRate = 0.1f; // 속도 증가 비율
    public float resetPosition = -10f; // 초기 위치로 리셋할 x 좌표
    public float verticalRange = 3f; // 위아래 움직임 범위
    public float verticalSpeed = 2f; // 위아래 움직임 속도

    private float speed; // 현재 속도
    private bool movingUp = true; // 위로 움직이는 중인지 여부

    public void Start()
    {
        speed = startSpeed;
    }

    public void Update()
    {
        // 오른쪽에서 왼쪽으로 일정한 속도로 움직이기
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // 장애물이 왼쪽 끝까지 이동한 경우 위치 초기화
        if (transform.position.x <= resetPosition)
        {
            ResetPosition();
        }

        // 위아래로 움직이기
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

        // 시간이 지날수록 속도 증가
        speed += speedIncreaseRate * Time.deltaTime;
    }

    public void ResetPosition()
    {
        // 초기 위치로 리셋
        Vector3 startPosition = new Vector3(10f, Random.Range(-verticalRange, verticalRange), transform.position.z);
        transform.position = startPosition;
    }
}
