using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ScoreLoader : MonoBehaviour
{
    public GameObject ScorePrefab;
    public Transform ScoreBoard;

    private struct HighScore
    {
        public string Name;
        public int Score;
    }

    private List<HighScore> highScores;
    void Start()
    {
        InitializeScoreboard();       
    }

	private void InitializeScoreboard()
	{
        // TODO: Load high scores from save file
        highScores = LoadMockHighScores();

        // TODO: check if this score deserves to be in the list. Then have a input pop up for the user to enter their name.
        CreateNewScore("MyCurrentScore", GameManager.Instance.GetPoints());

        highScores = highScores.OrderByDescending(x => x.Score).ToList();

        // initialize the prefabs for each high score
        foreach (var score in highScores)
        {
            var prefab = Instantiate(ScorePrefab, ScoreBoard);
            var children = prefab.GetComponentsInChildren<Text>();
            children[0].text = score.Name;
            children[1].text = score.Score.ToString();
        }
    }

	public void CreateNewScore(string name, int score)
	{
        highScores.Add(new HighScore()
        {
            Name = name,
            Score = score
        });
	}

    /// <summary>
    /// Mock high scores to test the high score screen. We should load these from a save file at some point.
    /// </summary>
    /// <returns>List of high scores</returns>
    private List<HighScore> LoadMockHighScores()
	{
        return new List<HighScore>()
        {
            new HighScore()
            {
                Name = "Suck",
                Score = 100
            },
            new HighScore()
            {
                Name = "Best",
                Score = 10000
            },
            new HighScore()
            {
                Name = "Better",
                Score = 5000
            },
            new HighScore()
            {
                Name = "Good",
                Score = 2500
            }
        };
    }
}
