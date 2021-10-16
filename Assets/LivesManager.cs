using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
	public static LivesManager Instance;

	public Transform LifeContainer;
	public GameObject LifeImage;

	private int lives = 3;

	void Awake()
	{

		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		InitializeLives();
	}

	public void InitializeLives()
	{
		if (GameManager.Instance.gameMode == GameMode.FreePlay && lives > 0)
		{
			for (int i = 0; i < lives; i++)
			{
				Instantiate(LifeImage, LifeContainer);
			}
		}
	}
	public void UseLife()
	{
		if (GameManager.Instance.gameMode == GameMode.FreePlay)
		{
			if (lives > 0)
			{
				lives--;
				var lifeToRemove = LifeContainer.GetChild(0);
				Destroy(lifeToRemove.gameObject);
			}
			if (lives == 0)
			{
				//game over
				SceneManager.LoadScene("HighScoreScreen");
			}
		}
	}

}
