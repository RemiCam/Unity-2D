using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float laserSpeed = 25f;
    [SerializeField] int laserDmg = 10;
    [SerializeField] AudioClip fired;
    [SerializeField] float firedVol = 1;
    [SerializeField] float bombRotation = 360; //deg / sec

    private void Update()
    {
        if (tag == "Bomb") { transform.Rotate(new Vector3(0, 0, bombRotation * Time.deltaTime)); }
    }

    private void Start()
    {
        AudioSource.PlayClipAtPoint(fired, Camera.main.transform.position, firedVol);
    }

    public float GetLaserSpeed()
    {
        return laserSpeed;
    }

    public int GetLaserDmg() { return laserDmg; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}