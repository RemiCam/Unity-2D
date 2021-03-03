using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master volume";
    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;
    const float defaultVolume = 0.8f;
    const string DIFFICULTY_KEY = "difficulty";
    const int MIN_DIFFICULTY = 1;
    const int MAX_DIFFICULTY = 10;

    public static void SetMasterVolume(float volume)
    {
        if(volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            Debug.Log("Master volume set to " + volume);
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume is out of range");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, defaultVolume);
    }

    public static void SetDifficulty(int difficulty)
    {
        if (difficulty >= MIN_DIFFICULTY && difficulty <= MAX_DIFFICULTY)
        {
            Debug.Log("Difficulty set to " + difficulty);
            PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Difficulty is out of range");
        }
    }

    public static int GetDifficulty()
    {
        return PlayerPrefs.GetInt(DIFFICULTY_KEY);
    }
}
