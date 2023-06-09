using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMove : MonoBehaviour
{
    public float scrollspeed; // 초기 스크롤 속도
    public float plus; // 가속도

    private float currentScrollSpeed; // 현재 스크롤 속도
    private float targetOffset;
    private Renderer m_Renderer;

    public void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        currentScrollSpeed = scrollspeed;
    }

    // Update is called once per frame
    public void Update()
    {
        targetOffset -= Time.deltaTime * currentScrollSpeed;
        m_Renderer.material.mainTextureOffset = new Vector2(targetOffset, 0);

        // 가속도를 적용하여 스크롤 속도를 증가시킵니다.
        currentScrollSpeed += plus * Time.deltaTime;
    }
}