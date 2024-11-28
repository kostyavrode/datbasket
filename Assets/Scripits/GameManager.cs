using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action onGameStarted;
    private bool isGameStarted;
    private float currentTimeScale;
    private int score;
    private int money;
    private float time = 60;
    public int targetGoal;
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private AudioSource audioSourceGoal;
    private void Awake()
    {
        instance = this;
        currentTimeScale = Time.timeScale;
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.Save();
        }
        
    }
    private void Start()
    {
        UIManager.instance.ShowMoney(money.ToString());
    }
    private void Update()
    {
        if (isGameStarted)
        {
            time -= Time.deltaTime;
            UIManager.instance.ShowTime((int)time);
            UIManager.instance.ShowScore(score.ToString());
        }
        if (time<=0 && isGameStarted)
        {
            EndGame(false);
        }
    }
    public void AddScore()
    {
        score += 1;
        audioSourceGoal.Play();
        UIManager.instance.ShowScore(score.ToString());
        StartCoroutine(WaitForGenerateNewHoopPos());
        if (score >= targetGoal)
        {
            EndGame(true);
        }
    }
    public void UpdateMoney()
    {
        money = PlayerPrefs.GetInt("Money");
        UIManager.instance.ShowMoney(money.ToString());
    }
    public void SetTargetGoals(int t)
    {
        targetGoal = t;
    }
    public void StartGame()
    {
        isGameStarted = true;
        onGameStarted?.Invoke();
        Time.timeScale = 1f;
        foreach(GameObject obj in objectsToSpawn)
        {
            obj.SetActive(true);
        }
    }
    public void PauseGame()
    {
        isGameStarted = false;
        Time.timeScale = 0f;
    }
    public void UnPauseGame()
    {
        isGameStarted = true;
        Time.timeScale = currentTimeScale;
    }
    public void EndGame(bool isWin=false)
    {
        isGameStarted = false;
        int temo = PlayerPrefs.GetInt("Money") + score;
        PlayerPrefs.SetInt("Money", temo);
        PlayerPrefs.Save();
        CheckBestScore();
        
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        if (isWin)
        {
            Debug.Log("Level Completed");
            UIManager.instance.ShowWinPanel();
            PlayerPrefs.SetInt("Levels", PlayerPrefs.GetInt("Levels") + 1);
        }
        else
        {
            UIManager.instance.ShowLosePanel();
        }
    }
    private void CheckBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            int tempBestScore = PlayerPrefs.GetInt("BestScore");
            if (tempBestScore > score)
            {
                UIManager.instance.ShowBestScore(tempBestScore.ToString());
            }
            else
            {
                UIManager.instance.ShowBestScore(score.ToString());
                PlayerPrefs.SetInt("BestScore", score);
                PlayerPrefs.Save();
            }
        }
        else
        {
            UIManager.instance.ShowBestScore(score.ToString());
            PlayerPrefs.SetInt("BestScore", score);
            PlayerPrefs.Save();
        }
    }
    private IEnumerator WaitForGenerateNewHoopPos()
    {
        yield return new WaitForSeconds(1);
        GateController.instance.ChangePosition();
    }
    public bool IsGameStarted()
    {
        return isGameStarted;
    }
}
