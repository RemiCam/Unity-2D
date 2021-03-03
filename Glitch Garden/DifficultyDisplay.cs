using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyDisplay : MonoBehaviour
{
    int difficulty;
    TextMeshProUGUI difficultyText;

    private void Start()
    {
        difficultyText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        difficulty = (int)GetComponentInParent<Slider>().value;
        difficultyText.text = difficulty.ToString();
    }
}
