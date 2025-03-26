using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;

    public GameObject gameOverPanel;

    private void OnEnable()
    {
        EventHandler.GetPointEvent += OnGetPointEvent;
    }

    private void OnDisable()
    {
        EventHandler.GetPointEvent -= OnGetPointEvent;
    }

    private void OnGetPointEvent(int point)
    {
        scoreText.text = point.ToString();
    }

    private void Start()
    {
        scoreText.text = "00";
    }
}
