using UnityEngine;

public class LifeManager : MonoBehaviour
{
	public Transform LifeContainer;
	public GameObject LifeImage;
	private void Start()
	{
		InitializeLives();
	}

	public void InitializeLives()
	{
		if (GameManager.Instance.useLives && GameManager.Instance.GetLives() > 0)
		{
			Instantiate(LifeImage, LifeContainer);
		}
	}
}
