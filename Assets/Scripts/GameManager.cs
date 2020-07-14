using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int livesAmount;
    public float roundTime;
    public float breakTime;

    [SerializeField] Color redColor;
    [SerializeField] Color greenColor;
    [SerializeField] GameUiController uiController;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] int minHealPerLvl;
    [SerializeField] int maxHealPerLvl;
    [SerializeField] GameObject gameOverPanel;

    private bool gameIsActive;
    private int waveCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGame", 3f);
    }

    void StartGame()
    {
        gameIsActive = true;
        Time.timeScale = 1;
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        var roundTimer = roundTime;
        var breakTimer = breakTime;

        if(waveCounter % 3 == 0)
        {
            enemySpawner.IncreaseAmountToSpawn(1);
        }

        enemySpawner.StartSpawn();
        //game plays
        while (roundTimer > 0)
        {
            roundTimer = Mathf.Max(0f, roundTimer - Time.deltaTime);
            uiController.SetTimerText(Mathf.RoundToInt(roundTimer), redColor);
            uiController.SetDescriptionText("Wave " + waveCounter.ToString(), redColor);
            uiController.SetTimerImage(true);
            yield return null;
        }

        enemySpawner.StopSpawn();
        //little break
        while (breakTimer > 0)
        {
            breakTimer = Mathf.Max(0f, breakTimer - Time.deltaTime);
            uiController.SetTimerText(Mathf.RoundToInt(breakTimer), greenColor);
            uiController.SetDescriptionText("Take a breath!", greenColor);
            uiController.SetTimerImage(false);
            yield return null;
        }
        //start again
        HealHP(GetRandomNum(minHealPerLvl, waveCounter*2));
        waveCounter++;
        StartCoroutine(GameLoop());
    }

    public void HealHP(int amount)
    {
        livesAmount += amount;
        uiController.ShowHpInfoTxt("+" + amount.ToString(), greenColor);
    }

    public int GetRandomNum(int from, int to)
    {
        return UnityEngine.Random.Range(from, to + 1);
    }

    public void DecreaseHp(int amount)
    {
        livesAmount = Mathf.Max(0, livesAmount - amount);
        uiController.ShowHpInfoTxt("-" + amount.ToString(), redColor);
    }

    private void Update()
    {
        uiController.SetHpText(livesAmount.ToString());
        GameOverCheck();
    }

    private void GameOverCheck()
    {
        if(livesAmount <= 0 && gameIsActive)
        {
            gameIsActive = false;
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        gameOverPanel.SetActive(true);
/*        Time.timeScale = 0;*/
    }
}
