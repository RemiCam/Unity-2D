using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    //config params
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int ptsPerBlock = 5;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled = false;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] int lifeCount = 3;

    //cached ref
    Level level;
    Paddle paddle;


    //state vars
    [SerializeField] int currentScore = 0;
    [Range(0.1f, 8f)][SerializeField] float paddleScale = 3f;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        level = FindObjectOfType<Level>();
        scoreText.text = currentScore.ToString();

        lifeCount--;
        lifeText.text = level.GetBallInPlay().ToString();

        paddle = FindObjectOfType<Paddle>();
        paddle.transform.localScale = new Vector3(paddleScale, paddle.transform.localScale.y, paddle.transform.localScale.z);
    }

    private void Update()
    {
        Time.timeScale = gameSpeed;
        scoreText.text = currentScore.ToString();
        lifeText.text = lifeCount.ToString();
    }

    public void AddLife() 
    {
        lifeCount++;
    }

    public void SubLife()
    {
        lifeCount--;
    }

    public int GetLifeCount()
    {
        return this.lifeCount;
    }

    public void AddToScore()
    {
        currentScore += ptsPerBlock;
    }

    public void ResetGame()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    public void ChangePaddleScale(float delta)
    {
        paddleScale -= delta;
        paddle.transform.localScale = new Vector3(paddleScale, paddle.transform.localScale.y, paddle.transform.localScale.z);
    }

    public void SetNewPaddle(Paddle newPaddle)
    {
        paddle = newPaddle;
    }

    public void UpdatePaddleScale()
    {
        paddle.transform.localScale = new Vector3(paddleScale, paddle.transform.localScale.y, paddle.transform.localScale.z);
    }
}
