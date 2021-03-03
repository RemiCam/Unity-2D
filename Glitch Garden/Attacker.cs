using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [Range(0f, 5f)] [SerializeField] float currentSpeed = 1f;
    GameObject currentTarget;

    private void Awake()
    {
        FindObjectOfType<LevelController>().AttackerSpawned();
    }

    private void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
        ResumeWalking();
    }

    private void OnDestroy()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        if (levelController != null) { levelController.AttackerDestroyed(); }
    }

    public void SetMouvementSpeed (float speed)
    {
        currentSpeed = speed;
    }

    public void Attack(GameObject target) 
    {
        GetComponent<Animator>().SetBool("isAttacking", true);
        currentTarget = target;
    }

    public void ResumeWalking()
    {
        if (!currentTarget) { GetComponent<Animator>().SetBool("isAttacking", false); }
    }

    public void StrikeCurrentTarget(int damage)
    {
        if (!currentTarget) { return; }
        Health health = currentTarget.GetComponent<Health>();
        if(health)
        {
            health.DealDamage(damage);
        }
    }

}
