using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartButtonClicked()
	{
		GameManager.Instance.gameMode = GameMode.Story;
		SceneManager.LoadScene("Level_1");
	}

	public void FreePlayButtonClicked()
	{
		GameManager.Instance.gameMode = GameMode.FreePlay;
		SceneManager.LoadScene("Level_1");
	}

	public void TimeAttackButtonClicked()
    {

    }

	public void QuitButtonClicked()
    {
		Application.Quit();
    }
}
