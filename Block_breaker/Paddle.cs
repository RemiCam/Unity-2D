using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //config params
    [SerializeField] float screenWidthInUnit = 16f;
    [SerializeField] Ball startingBall;

    //cached ref
    GameSession gameSession;

    //state
    float paddleHeight = 0.53f;
    
    // Start is called before the first frame update
    private void Start()
    {
        SpawnBall();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        float minMouseX = this.transform.localScale.x;
        float maxMouseX = screenWidthInUnit - this.transform.localScale.x;
        paddlePos.x = Mathf.Clamp(GetXPos(), minMouseX, maxMouseX);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        /*
        if (gameSession.IsAutoPlayEnabled())
        {
            return FindObjectOfType<Ball>().transform.position.x;
        }
        else
        {
        */
            return Input.mousePosition.x / Screen.width * screenWidthInUnit;
        //}
    }

    public Ball SpawnBall()
    {
        Vector3 newBallPos = new Vector3(transform.position.x, paddleHeight + startingBall.GetBallRadius() , transform.position.z);
        Ball newBall = Instantiate(startingBall, newBallPos, Quaternion.identity);
        newBall.LockBallToPaddle();
        return newBall;
    }

    public void ResetPaddle()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public float getPaddleHeight()
    {
        return this.paddleHeight;
    }
}
