using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [Header("Lose Label")]
    [SerializeField] GameObject loseLabel;
    [SerializeField] AudioClip loseSFX;
    [Range(0f, 1f)] [SerializeField] float loseSFXVolune = 1f;

    [Header("Win Label")]
    [SerializeField] GameObject winLabel;
    [SerializeField] AudioClip winSFX;
    [Range(0f,1f)][SerializeField] float winSFXVolune = 1f;
    [SerializeField] float winDelay = 3f;

    [Header("Debug")]
    [SerializeField] int attackerCount = 0; //debug
    [SerializeField] bool timeOver = false; //debug 

    private void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
    }

    public void AttackerSpawned()
    {
        attackerCount++;
    }

    public void AttackerDestroyed()
    {
        attackerCount--;
        LoadNextLevel();
    }

    public void TimeOver()
    {
        timeOver = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawnerArray = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }


    private void LoadNextLevel()
    {
        Life life = FindObjectOfType<Life>();
        if (life == null) { return; }
        if (timeOver && attackerCount <= 0 && life.GetLifeCount() > 0)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position, winSFXVolune);
        yield return new WaitForSeconds(winDelay);
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }

    
    public void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
        AudioSource.PlayClipAtPoint(loseSFX, Camera.main.transform.position, loseSFXVolune);
        Time.timeScale = 0;
    }

}
