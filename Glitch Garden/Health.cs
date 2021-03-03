using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] AudioClip deathSFX;
    [Range(0.1f, 1f)][SerializeField] float audioSound = 0.1f;
    [SerializeField] GameObject deathVFX;
    bool gettingDamaged = false;

    private void Update()
    {
        gettingDamaged = false;
    }


    public int GetHealth() { return health; }

    public void DealDamage(int damage)
    { 
        health -= damage;
        gettingDamaged = true;
        if (health <= 0)
        {
            TriggerDeathFX();
            Destroy(gameObject);
        }
    }

    public bool GetGettingDamaged() { return gettingDamaged; }

    private void TriggerDeathFX()
    {
        if (deathSFX == null || deathVFX == null) { Debug.Log("FX missing on " + gameObject); return; }
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, audioSound);
        GameObject newDeathVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(newDeathVFX, 1f);
    }
}
