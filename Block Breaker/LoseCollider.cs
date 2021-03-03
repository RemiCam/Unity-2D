using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    //config param
    [SerializeField] string nextScene;

    //cached ref
    Level level;
    GameSession gameSession;
    Paddle paddle;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        gameSession = FindObjectOfType<GameSession>();
        paddle = FindObjectOfType<Paddle>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        level.SubBallCount();
        Destroy(collision.gameObject);
        if (level.GetBallInPlay() <= 0)
        {
            if (gameSession.GetLifeCount() > 0)
            {
                gameSession.SubLife();
                paddle.SpawnBall();
            }
            else
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
