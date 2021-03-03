using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //config params
    //[Range(0,1)][SerializeField] float rotationSpeed = 1f;
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] int health = 20;
    [SerializeField] Laser laser;
    [SerializeField] Transform gun;
    [SerializeField] float firingDelay = 0.5f;
    [SerializeField] float firingRange = 5f;

    //cached refs
    const string MOVING_BOOL = "Moving";
    const string DIE_TRIGGER = "Die";
    Player player;
    Rigidbody2D rb;
    Animator animator;

    //states
    Vector2 toPlayerVect;
    [SerializeField] bool alive = true; //debug
    float firingCountdown;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        firingCountdown = firingDelay;
    }

    private void Update()
    {
        toPlayerVect = player.transform.position - transform.position;
        AnimationControl();
        RotateTowardPlayer();
        Fire();
    }

    private void FixedUpdate()
    {
        MoveTowardPlayer();
    }

    private void MoveTowardPlayer()
    {
        if (!alive) { rb.velocity = Vector2.zero; return; }
        rb.velocity = toPlayerVect.normalized * movementSpeed;
    }

    private void RotateTowardPlayer()
    {
        if (!alive) { return; }
        //transform.rotation = Quaternion.Slerp(transform.rotation, ToPlayerAngle(), Time.deltaTime * rotationSpeed);
        transform.rotation = ToPlayerAngle();
    }

    private Quaternion ToPlayerAngle()
    {
        float toPlayerAngle = Mathf.Atan2(toPlayerVect.y, toPlayerVect.x) * Mathf.Rad2Deg;
        Quaternion toPlayerRotation = Quaternion.AngleAxis(toPlayerAngle, Vector3.forward);
        return toPlayerRotation;
    }

    private void AnimationControl()
    {
        animator.SetBool(MOVING_BOOL, alive);
        if (!alive) { animator.SetTrigger(DIE_TRIGGER); }
    }

    private void OnTriggerEnter2D(Collider2D collision)  //handle death
    {
        Laser dmgDealer = collision.GetComponent<Laser>();
        if (!dmgDealer) { Debug.LogError("Wrong collision"); }
        health -= dmgDealer.GetLaserDmg();
        if (health <= 0) 
        { 
            alive = false;
            GetComponent<Collider2D>().enabled = false;
        }

    }

    private void Fire()
    {
        firingCountdown -= Time.deltaTime;
        bool fireEnabled = toPlayerVect.magnitude <= firingRange;
        if(fireEnabled && firingCountdown <= 0 && alive)
        {
            Laser newLaser = Instantiate(laser, gun.position, ToPlayerAngle());
            newLaser.GetComponent<Rigidbody2D>().velocity = toPlayerVect.normalized * (newLaser.GetLaserSpeed());
            firingCountdown = firingDelay;
        }
    }
}