using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int score;
    public List<int> scoreList;
    private string dataPath;

    private void Awake()
    {
        dataPath = Application.persistentDataPath + "/leaderboard.json";
        scoreList = GetScoreListData();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
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
        score = point;
    }

    private void OnGameOverEvent()
    {
        if (!scoreList.Contains(score))
        {
            scoreList.Add(score);
        }

        scoreList.Sort();
        scoreList.Reverse();

        File.WriteAllText(dataPath, JsonConvert.SerializeObject(scoreList));
    }

    public List<int> GetScoreListData()
    {
        if (File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            return JsonConvert.DeserializeObject<List<int>>(jsonData);
        }

        return new List<int>();
    }
}
