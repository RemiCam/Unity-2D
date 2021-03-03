using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;

    private void Awake()
    {
        SetUpSingleton();
    }
    
    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
        DontDestroyOnLoad(gameObject);
    }
}

    public int GetScore() { return score; }

    public void AddScore(int scoreValue) { score += scoreValue; }

    public void ResetGame() { Destroy(gameObject); }
}
