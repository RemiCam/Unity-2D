using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config params
    [SerializeField] float runSpeed = 3f;
    [SerializeField] Laser Laser;
    [SerializeField] float firingDelay = 0.2f;
    [SerializeField] float dashDist = 3f;
    [SerializeField] AudioClip dashSound;
    [SerializeField] float dashVol = 0.1f;
    [SerializeField] Transform gun;
    [SerializeField] int health = 100;

    //cached refs
    const string RUNNING_BOOL = "Running";
    const string FIRE_BOOL = "Firing";
    Rigidbody2D rb;
    Animator animator;

    //states
    Coroutine firingCoroutine;
    Vector2 mov;
    bool dash;
    float firingCountdown;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        firingCountdown = firingDelay;
    }


    private void Update()
    {
        MoveInput();
        DashInput();
        Rotate();
        Fire();
    }


    private void FixedUpdate()
    {
        Move();
        Dash();
    }

    private void MoveInput()
    {
        mov = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool playerRunning = Mathf.Abs(rb.velocity.magnitude) > 0;
        animator.SetBool(RUNNING_BOOL, playerRunning);
    }

    private void Move()
    {
        rb.velocity = new Vector2(mov.x, mov.y) * runSpeed;
    }

    private void DashInput()
    {
        if (Input.GetButtonDown("Jump")) 
        { 
            AudioSource.PlayClipAtPoint(dashSound,Camera.main.transform.position, dashVol); 
            dash = true; 
        }
        if (Input.GetButtonUp("Jump")) { dash = false; }
    }
    private void Dash()
    {
        if (dash)
        {
            rb.velocity *= dashDist;
        }
    }

    private void Rotate()
    {
        transform.rotation = PlayerToMouseAngle();
    }

    private Vector3 RelativeMousePos()
    {
        Camera mainCamera = Camera.main;
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativePos = mousePos - transform.position;
        return relativePos;
    }

    private Quaternion PlayerToMouseAngle()
    {
        Vector3 relativePos = RelativeMousePos();
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        return rotation;
    }


    private void Fire()
    {
        firingCountdown -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContuniously());
            animator.SetBool(FIRE_BOOL, true);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            animator.SetBool(FIRE_BOOL, false);
        }
    }

    IEnumerator FireContuniously()
    {
        while (true)
        {
            if (firingCountdown <= 0)
            {
                Laser newLaser = Instantiate(Laser, gun.position, PlayerToMouseAngle());
                newLaser.GetComponent<Rigidbody2D>().velocity = RelativeMousePos().normalized * (newLaser.GetLaserSpeed() + rb.velocity.magnitude);
                firingCountdown = firingDelay;
            }
            yield return new WaitForSeconds(firingDelay);
        }
    }
}