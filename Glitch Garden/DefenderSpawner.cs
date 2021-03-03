using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defender;
    GameObject defenderParent;
    const string DEFENDER_PARENT_NAME = "Defenders";

    private void Start()
    {
        CreateDefenderParent();
    }

    private void CreateDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);
        if (!defenderParent)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        }
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    public void SetSelectedDefender(Defender defenderToSelect) 
    { 
        defender = defenderToSelect; 
    }

    private void AttemptToPlaceDefenderAt (Vector2 gridPos)
    {
        if (!defender) { return; }

        var starDisplay = FindObjectOfType<StarDisplay>();
        int defenderCost = defender.GetStarCost();

        if (defenderCost > starDisplay.GetStars()) { return; }

        starDisplay.SubStars(defenderCost);
        SpawnDefender(gridPos);
    }

    private void SpawnDefender(Vector2 worldPos)
    {
        Defender newDefender = Instantiate(defender, worldPos, Quaternion.identity);
        newDefender.transform.parent = defenderParent.transform;
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        worldPos = new Vector2(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));
        return worldPos;
    }
}