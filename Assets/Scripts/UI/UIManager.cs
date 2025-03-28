using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text scoreText;

    public GameObject gameOverPanel;

    private void OnEnable()
    {
        Time.timeScale = 1;
        EventHandler.GetPointEvent += OnGetPointEvent;
        EventHandler.GameOverEvent += OnGameOverEvent;
    }

    private void OnDisable()
    {
        EventHandler.GetPointEvent -= OnGetPointEvent;
        EventHandler.GameOverEvent -= OnGameOverEvent;
    }

    private void OnGetPointEvent(int point)
    {
        scoreText.text = point.ToString();
    }

    private void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true);
        if (gameOverPanel.activeInHierarchy)
        {
            // 游戏暂停
            Time.timeScale = 0;
        }
    }

    private void Start()
    {
        scoreText.text = "00";
    }

    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        TransitionManager.instance.Transition("Gameplay");
    }

    public void BackToMenu()
    {
        gameOverPanel.SetActive(false);
        TransitionManager.instance.Transition("Title");
    }
}
