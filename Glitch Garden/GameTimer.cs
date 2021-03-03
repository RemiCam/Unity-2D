using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Level timer in seconds")]
    [SerializeField] float levelTime = 10f;
    bool triggeredLevelFinished = false;

    private void Update()
    {
        if (triggeredLevelFinished) { return; }
        GetComponent<Slider>().value = Time.timeSinceLevelLoad / levelTime;
        triggeredLevelFinished = TimeUp();
    }

    public bool TimeUp()
    {
        if (Time.timeSinceLevelLoad >= levelTime) 
        {
            FindObjectOfType<LevelController>().TimeOver();
            return true; 
        }
        else { return false; }
    }
}
