using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //params
    [SerializeField] int breakableBlocks = 0;   //visible for debugging
    [SerializeField] int ballsInPlay;   //visible for debugging

    //cached ref
    SceneLoader sceneLoader;
    GameSession gameSession;
    Paddle paddle;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameSession = FindObjectOfType<GameSession>();
        ballsInPlay = 0;
        paddle = FindObjectOfType<Paddle>();
        gameSession.SetNewPaddle(paddle);
        gameSession.UpdatePaddleScale();
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void AddBallCount()
    {
        ballsInPlay++;
        gameSession.AddLife();
    }

    public void SubBallCount()
    {
        ballsInPlay--;
        gameSession.SubLife();
    }

    public int GetBallInPlay()
    {
        return ballsInPlay;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if(breakableBlocks <= 0)
        {
            gameSession.SubLife();
            sceneLoader.LoadNextScene();
        }
    }
}
