using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float scrollSpeed = -1.5f;
    public bool gameIsOver = false;
    public bool unlimitedGame = false;

    private int m_Score = 0;
    private int m_LastScore = 0;
    public static int highScore = 0;
    public float timeLeft;
    public int ballonLeft;


    public Text scoreText;
    public Text lastScoreText;
    public Text highScoreText;
    public Text timeLeftText;
    public Text ballonLeftText;

    public GameObject gameOverText;
    public GameObject lastScoreTag;
    public GameObject highScoreTag;
    public GameObject ButtonDie;
    public GameObject ButtoNextLevel;
    public GameObject scoreTextAlive;
    public GameObject levelCompleted;
    public GameObject congratulationsEndLevel;
    public GameObject finishedButFailedTag;
    public GameObject pauseButton;
    public GameObject resumeButton;



    public bool setHighScore = false;
    public bool setLevelToOne = false;

    // For ballon
    public Color currentColorBallon = Color.red;
    public Color nextColorBallon = Color.blue;
    // For RandomPlane
    public int ChangeSpawnRatePlane = 15;
    public int ChangeSpawnRateBulding = 10;
    // For jet
    public bool isJet = false;
    public int sequenceOfScore = 0;
    // For Scrolling
    public float speedChangeEffect = 1f;
    // For paperPlane
    public bool collisionWithAsset = false;
    // For paused button
    public bool paused = false;

    public int currentLevel = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (setHighScore) highScore = 0; // Only for setting the High Score for submission
            else highScore = PlayerPrefs.GetInt("highScore", 0);
            if (setLevelToOne) currentLevel = 1; // Only for setting the level for submission
            else currentLevel = PlayerPrefs.GetInt("level", 1);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    [System.Obsolete]

    public void Update()
    {
        if (!gameIsOver && timeLeft >= 0 && !unlimitedGame) {
            timeLeft -= Time.deltaTime;
            ballonLeftText.text = "Ballon Left: " + ballonLeft;
            timeLeftText.text = "Time Left: " + (int)timeLeft;
        }
    }

    private bool touchFinishLine = false;
    public void FinishLineCrossed()
    {
        if (touchFinishLine) return;
        touchFinishLine = true;
        if (ballonLeft > 0)
        {
            finishedButFailedTag.SetActive(true);
            pauseButton.SetActive(false);
            resumeButton.SetActive(false);
            gameIsOver = true;
            gameOverText.SetActive(false);
            ButtonDie.SetActive(true);
            return;
        }
        currentLevel++;
        pauseButton.SetActive(false);
        resumeButton.SetActive(false);
        congratulationsEndLevel.SetActive(true);
        levelCompleted.SetActive(true);
        ButtoNextLevel.SetActive(true);
        gameIsOver = true;
    }


    public void Die()
    {
        if (gameIsOver) return;
        pauseButton.SetActive(false);
        resumeButton.SetActive(false);
        gameOverText.SetActive(true);
        highScoreTag.SetActive(true);
        lastScoreTag.SetActive(true);
        ButtonDie.SetActive(true);
        scoreTextAlive.SetActive(false);


        lastScoreText.text = "LAST SCORE: " + m_LastScore;
        highScoreText.text = "HIGH SCORE: " + highScore;
        PlayerPrefs.SetInt("highScore", highScore);
        gameIsOver = true;
    }

    public void Score()
    {
        if (gameIsOver) return;
        m_Score++;
        sequenceOfScore++;
        if (ballonLeft != 0) ballonLeft--;
        if(!unlimitedGame) ballonLeftText.text = "Ballon Left: " + ballonLeft;
        scoreText.text = "SCORE: " + m_Score;
        m_LastScore = m_Score;
        if (m_Score > highScore)
        {
            highScore = m_Score;
        }
        ChangeSpawnRate();
    }

    public int getScore()
    {
        return m_Score;
    }

    public void nextColor()
    {
        currentColorBallon = nextColorBallon;
        nextColorBallon = Random.ColorHSV();
    }

    public void ChangeSpawnRate()
    {
        if (m_Score % 10 == 0 && m_Score != 0 && m_Score <= 70)
        {
            if(ChangeSpawnRatePlane > 3)ChangeSpawnRatePlane--;
            if (ChangeSpawnRateBulding > 3) ChangeSpawnRateBulding--;
            if (m_Score % 20 == 0 && ChangeSpawnRatePlane > 3) ChangeSpawnRatePlane--; // Every 20 point changeSpawnRate - 2
        }
    }
    public void SetChangeSpawnRatePlane(int setChange)
    {
        ChangeSpawnRatePlane = setChange;
    }
    public void SetChangeSpawnRateBulding(int setChange)
    {
        ChangeSpawnRateBulding = setChange;
    }
    public void SetSpeedChangeEffect(float sliceByNSeconds)
    {
        if (speedChangeEffect <= 3) speedChangeEffect += 1 / sliceByNSeconds;
    }
    public void SetJetActive(bool isActive)
    {
        isJet = isActive;
    }
    public void SetCollisionWithAsset()
    {
        collisionWithAsset = true;
    }
    public int getLevel()
    {
        return currentLevel;
    }
}