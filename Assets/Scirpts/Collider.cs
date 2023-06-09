using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collider : MonoBehaviour
{
    public float minX, maxX;
    public float minY, maxY;
    public float minZ, maxZ;

    public Transform target;

    public new Renderer renderer;
    public GameManager gameManager;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Update()
    {
        minX = target.position.x - (target.localScale.x / 2);
        maxX = target.position.x + (target.localScale.x / 2);
        minY = target.position.y - (target.localScale.y / 2);
        maxY = target.position.y + (target.localScale.y / 2);
        minZ = target.position.z - (target.localScale.z / 2);
        maxZ = target.position.z + (target.localScale.z / 2);

        bool isColliding = transform.position.x + (transform.localScale.x / 2) >= minX &&
                           transform.position.x - (transform.localScale.x / 2) <= maxX &&
                           transform.position.y + (transform.localScale.y / 2) >= minY &&
                           transform.position.y - (transform.localScale.y / 2) <= maxY &&
                           transform.position.z + (transform.localScale.z / 2) >= minZ &&
                           transform.position.z - (transform.localScale.z / 2) <= maxZ;

        if (isColliding)
        {
            gameManager.GameOver();
            //renderer.material.color = Color.red;
        }
        else
        {
            //renderer.material.color = Color.green;
        }

    }
}




