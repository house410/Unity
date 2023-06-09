using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMove : MonoBehaviour
{
    public float scrollspeed; // �ʱ� ��ũ�� �ӵ�
    public float plus; // ���ӵ�

    private float currentScrollSpeed; // ���� ��ũ�� �ӵ�
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

        // ���ӵ��� �����Ͽ� ��ũ�� �ӵ��� ������ŵ�ϴ�.
        currentScrollSpeed += plus * Time.deltaTime;
    }
}