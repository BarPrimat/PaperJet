using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
    }

    public void SetLevelTo1()
    {
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene(1);
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("level", GameManager.Instance.getLevel());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevelGame()
    {
        PlayerPrefs.SetInt("highScore", GameManager.highScore);
        PlayerPrefs.SetInt("level", GameManager.Instance.getLevel());

        SceneManager.LoadScene(GameManager.Instance.getLevel());
    }

    public void Paused()
    {
        if (GameManager.Instance.paused)
        {
            Time.timeScale = 1.0f;
            GameManager.Instance.paused = false;
            GameManager.Instance.resumeButton.SetActive(false);
            GameManager.Instance.pauseButton.SetActive(true);
        }
        else
        {
            Time.timeScale = 0.0f;
            GameManager.Instance.paused = true;
            GameManager.Instance.pauseButton.SetActive(false);
            GameManager.Instance.resumeButton.SetActive(true);
        }
    }

}
