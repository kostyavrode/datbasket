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
            EndGame();
        }
    }
    public void AddScore()
    {
        score+=1;
        audioSourceGoal.Play();
        UIManager.instance.ShowScore(score.ToString());
        StartCoroutine(WaitForGenerateNewHoopPos());
    }
    public void UpdateMoney()
    {
        money = PlayerPrefs.GetInt("Money");
        UIManager.instance.ShowMoney(money.ToString());
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
    public void EndGame()
    {
        isGameStarted = false;
        int temo = PlayerPrefs.GetInt("Money") + score;
        PlayerPrefs.SetInt("Money", temo);
        Debug.Log(temo);
        PlayerPrefs.Save();
        
        CheckBestScore();
        UIManager.instance.ShowLosePanel();
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
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
