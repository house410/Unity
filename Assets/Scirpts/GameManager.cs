using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent Player;
    public UnityEvent Enemy;
    public UnityEvent Game;

    public TextMeshPro gameoverText;
    public TextMeshPro scoreText;
    public TextMeshPro rankingText;

    public GameObject startImage;
    public GameObject scoreboard;
    public GameObject restartImage;
    public GameObject character;
    public GameObject obstacle;
    public GameObject moveobstacle;

    public int score;
    public bool ready;
    public bool end;
    public bool stop;
    public float scoreUpdate = 1f;

    private bool isGameOver = false;
    private bool isTimer = false;
    private float timer = 0f;
    private List<int> ranking = new List<int>();
    private List<int> initialRanking = new List<int>();

    public void Start()
    {
        score = 0;
        ready = true;
        end = false;
        gameoverText.gameObject.SetActive(false);
        rankingText.gameObject.SetActive(false);
        scoreText.text = "Score : " + score.ToString();
        rankingText.text = "Ranking: ";
        character.SetActive(false);
        obstacle.SetActive(false);
        moveobstacle.SetActive(false);
        scoreboard.SetActive(false);
        LoadRanking(); // 랭킹 로드
        Enemy.Invoke();
        Player.Invoke();
        Game.Invoke();

    }

    // 점수 추가 및 업데이트
    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();

        // 점수가 업데이트될 때마다 랭킹 정렬
        ranking.Add(score);
        ranking.Sort();
        ranking.Reverse();

        // 랭킹 텍스트 업데이트
        UpdateRankingText();
    }


    // 랭킹 텍스트 업데이트
    public void UpdateRankingText()
    {
        rankingText.text = "Ranking:\n";
        for (int i = 0; i < Mathf.Min(10, ranking.Count); i++)
        {
            rankingText.text += (i + 1) + ". " + ranking[i].ToString() + "\n";
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            gameoverText.gameObject.SetActive(true);
            rankingText.gameObject.SetActive(true);
            scoreboard.SetActive(true);
            restartImage.SetActive(true);
            Debug.Log("Game Over!");

            Time.timeScale = 0f;

            // 현재 점수가 기록되었으면 랭킹에 추가
            if (!ranking.Contains(score))
            {
                ranking.Add(score);
                ranking.Sort();
                ranking.Reverse();
                SaveRanking(); // 현재 스코어가 추가된 후 다시 랭킹 저장
            }

            // 랭킹 텍스트 업데이트
            UpdateRankingText();
            CancelInvoke("ScoreUpdate");
        }
    }
    public void LoadRanking()
    {
        ranking.Clear();
        for (int i = 0; i < 10; i++)
        {
            int score = PlayerPrefs.GetInt("Ranking_" + i.ToString(), 0);
            ranking.Add(score);
        }
        initialRanking.AddRange(ranking);
    }

    public void SaveRanking()
    {
        for (int i = 0; i < ranking.Count; i++)
        {
            PlayerPrefs.SetInt("Ranking_" + i.ToString(), ranking[i]);
        }
        PlayerPrefs.Save(); // 변경 사항 저장
    }

    public void ResetRanking()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.DeleteKey("Ranking_" + i.ToString());
        }
    }

    public void Pause()
    {
        if (stop == false)
        {
            Time.timeScale = 0f;
            stop = true;
        }
        else
        {
            stop = false;
            Time.timeScale = 1f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        isGameOver = false;
        isTimer = false;
        score = 0;
        scoreText.text = "Score : " + score.ToString();
        restartImage.SetActive(false);
        character.SetActive(false);
        obstacle.SetActive(false);
        moveobstacle.SetActive(false);
        gameoverText.gameObject.SetActive(false);
        rankingText.gameObject.SetActive(false);
        scoreboard.SetActive(false);
        ready = true;
        end = false;
        timer = 0f;
    }
    public void Update()
    {
        if (!isGameOver && isTimer) // 게임 오버가 아니고 타이머가 동작 중인 경우에만 업데이트 수행
        {
            timer += Time.deltaTime;

            if (timer >= scoreUpdate)
            {
                score++;
                scoreText.text = "Score: " + score.ToString();
                timer = 0f;
            }
        }

        if (Input.GetMouseButtonDown(0) && ready == true)
        {
            ready = false;
            startImage.SetActive(false);
            // 캐릭터와 장애물의 이동을 시작합니다.s
            character.SetActive(true);
            obstacle.SetActive(true);
            moveobstacle.SetActive(true);
            isTimer = true; // 타이머 동작 시작
        }
        else if (Input.GetMouseButtonDown(0) && ready == false)
        {
            // 이미 시작된 상태에서는 클릭해도 반응하지 않습니다.
        }

        if (Input.GetMouseButtonDown(0) && restartImage.activeSelf)
        {
            RestartGame();
            UpdateRankingText();
        }

    }
}