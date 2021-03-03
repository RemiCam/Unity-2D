using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Config params
    Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSound;
    [SerializeField] float randomFactor = 0.2f;

    //state
    bool hasStarted = false;
    float ballRadius = 0.22f;

    //cahched componenet references
    AudioSource myAudioSource;
    Rigidbody2D ballRigidBody2D;
    Level level;

    private void Awake()
    {
        paddle1 = FindObjectOfType<Paddle>();
    }

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        ballRigidBody2D = GetComponent<Rigidbody2D>();
        level = FindObjectOfType<Level>();
        CountBalls();
    }

    private void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void CountBalls()
    {
        level.AddBallCount();
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            ballRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    public void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, transform.position.y) ;
        transform.position = paddlePos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (UnityEngine.Random.Range(0f, randomFactor),
             UnityEngine.Random.Range(0f, randomFactor));

        if (hasStarted)
        {
            AudioClip clip = ballSound[UnityEngine.Random.Range(0, ballSound.Length)];
            myAudioSource.PlayOneShot(clip);
            ballRigidBody2D.velocity += velocityTweak;
        }
    }

    public void Launch()
    {
        hasStarted = true;

    }

    public float GetBallRadius()
    {
        return ballRadius;
    }
}
