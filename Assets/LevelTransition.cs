using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public GameObject currentLevel;
    public GameObject nextStoryLevel;
    public GameObject nextFreePlayLevel;
    public GameObject nextTimeAttackLevel;
    public float transitionTime;

    private GameObject nextLevel;

	private void Start()
	{
        if (GameManager.Instance.gameMode == GameMode.Story)
        {
            nextLevel = nextStoryLevel;
        }
        else if (GameManager.Instance.gameMode == GameMode.FreePlay)
        {
            nextLevel = nextFreePlayLevel;
        }
        else // time attack
        {
            nextLevel = nextTimeAttackLevel;
        }
    }

	public void SetActive(bool active)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = active;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            collision.GetComponent<Ball>().Suspend();
            StartCoroutine(DoStuff(collision.gameObject));
        }
    }

    private IEnumerator DoStuff(GameObject ball)
    {
        float currentTime = 0;
        Vector2 ballLocation = ball.transform.position;
        Vector2 ballTarget = transform.position;

        Vector2 directionToMoveBall = (ballTarget - ballLocation);

        List<SpriteRenderer> fadeOut = currentLevel.GetComponentsInChildren<SpriteRenderer>().ToList();
        List<SpriteRenderer> fadeIn = nextLevel.GetComponentsInChildren<SpriteRenderer>().ToList();


        foreach (SpriteRenderer sr in fadeIn)
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        nextLevel.SetActive(true);

        while (currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            if(Vector2.Distance(ballTarget, ballLocation) > 20)
            {
                ballLocation += directionToMoveBall * Time.deltaTime * 0.3f;
                ball.transform.position = ballLocation;
            }

            float alpha = currentTime / transitionTime;
            foreach(SpriteRenderer sr in fadeOut)
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1 - alpha);
            foreach (SpriteRenderer sr in fadeIn)
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);
        ball.GetComponent<Ball>().UnSuspend();

        currentLevel.SetActive(false);
        foreach (SpriteRenderer sr in fadeOut)
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        foreach (SpriteRenderer sr in fadeIn)
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);

        GameManager.Instance.StartNextLevel();
        yield break;
    }

    public void DoStuffInstantly()
    {
        currentLevel.SetActive(false);
        nextLevel.SetActive(true);
    }
}
