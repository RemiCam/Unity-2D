using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    bool spawn = true;
    [SerializeField] Attacker[] attackersPrefab;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;

    IEnumerator Start()
    {
        while(spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnAttacker();
        }
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    private void SpawnAttacker()
    {
        int randomIndex = Mathf.FloorToInt(Random.Range(0f, attackersPrefab.Length - Mathf.Epsilon));
        Attacker toSpawn = attackersPrefab[randomIndex];

        Attacker newAttacker = Instantiate(toSpawn,
            transform.position, 
            Quaternion.identity) as Attacker;
        newAttacker.transform.parent = transform;
    }
}
