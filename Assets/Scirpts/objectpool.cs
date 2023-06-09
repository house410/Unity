using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class objectpool : MonoBehaviour
{
    public List<GameObject> objectPool;

    public GameObject[] obstacle = new GameObject[2];

    public int poolSize = 10;

    float currentTime;
    public float createTime = 1.0f;
    float minTime = 2.5f;
    float maxTime = 4.5f;

    public void Start()
    {
        createTime = Random.Range(minTime, maxTime);
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(obstacle[Random.Range(0, 2)]);
            objectPool.Add(enemy);
            enemy.SetActive(false);
        }
    }

    public void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            if (objectPool.Count > 0)
            {
                GameObject enemy = objectPool[0];
                objectPool.Remove(enemy);

                enemy.SetActive(true);
            }
            currentTime = 0; // 현재 시간 초기화
            createTime = Random.Range(minTime, maxTime);
        }
    }
}
