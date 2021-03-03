using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarDisplay : MonoBehaviour
{
    [SerializeField] int stars = 100;
    TextMeshProUGUI starText;

    private void Start()
    {
        starText = GetComponent<TextMeshProUGUI>();
        UpdateDisplay();
    }
    private void UpdateDisplay()
    {
        starText.text = stars.ToString();
    }

    public int GetStars() { return stars; }

    public void AddStars(int amount) { stars += amount; UpdateDisplay(); }

    public void SubStars(int amount) 
    { 
        if(stars >= amount)
        {
            stars -= amount;
            UpdateDisplay();
        }
    }
}