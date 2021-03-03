using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] float audioVol = 1.0f;
    [SerializeField] GameObject blockVFX;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] bool plusOneBall = false;
    [SerializeField] bool paddleChanger = false;
    [SerializeField] float paddleTweak = 1f;

    //cached ref
    Level level;
    Ball ball;
    GameSession gameSession;
    //Paddle paddle;

    //state vars
    [SerializeField] int timesHit = 0;  //for debug

    private void Start()
    {
        CountBrekableBlocks();
        gameSession = FindObjectOfType<GameSession>();
        //paddle = FindObjectOfType<Paddle>();
    }

    private void CountBrekableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit(collision);
        }
    }

    private void HandleHit(Collision2D collision)
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock(collision);
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        { 
            Debug.LogError(this.name + " sprite is missing from array"); 
        }
    }

    private void DestroyBlock(Collision2D collision)
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerVFX();
        NewBall(collision);
        PaddleBlock();
    }

    private void PaddleBlock()
    {
        if(paddleChanger)
        {
            gameSession.ChangePaddleScale(paddleTweak);
        }
    }

    private void NewBall(Collision2D collision)
    {
        if (plusOneBall)
        {
            ball = FindObjectOfType<Ball>();
            Ball newBall = Instantiate(ball, collision.transform.position, collision.transform.rotation);
            newBall.Launch();
            newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.relativeVelocity.x ,collision.relativeVelocity.y);
        }
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, audioVol);
    }

    private void TriggerVFX()
    {
        GameObject sparkles = Instantiate(blockVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
