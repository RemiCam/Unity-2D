using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Life : MonoBehaviour
{
    [SerializeField] int baseLives = 11; //max diff is 10
    TextMeshProUGUI lifeText;
    int life;

    private void Start()
    {
        life = baseLives - PlayerPrefsController.GetDifficulty();
        lifeText = GetComponent<TextMeshProUGUI>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        lifeText.text = life.ToString();
    }

    public void SubLife()
    {
        life--;
        UpdateDisplay();
        if(life <= 0)
        {
            FindObjectOfType<LevelController>().HandleLoseCondition();
        }
    }

    public int GetLifeCount() { return life; }
}
