using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config params
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] float jumpSpeed = 28f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    //states
    bool isAlive = true;
    const string RUNNING_KEY = "Running";
    const string CLIMBING_KEY = "Climbing";
    const string DEATH_KEY = "Dying";

    //cached refs
    Rigidbody2D myRigidBody;
    float gravityScaleAtStart;
    Animator myAnimator;
    CapsuleCollider2D myColliderBody;
    BoxCollider2D myColliderFeet;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
        myAnimator = GetComponent<Animator>();
        myColliderBody = GetComponent<CapsuleCollider2D>();
        myColliderFeet = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
        Die();
    }

    private void Run()
    {
        float moveXPos = Input.GetAxis("Horizontal") * runSpeed;
        myRigidBody.velocity = new Vector2(moveXPos, myRigidBody.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool(RUNNING_KEY, playerHasHorizontalSpeed);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), transform.localScale.y);
        }
    }

    private void Jump()
    {
        if (!myColliderFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (Input.GetButtonDown("Jump"))
        {
            myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y + jumpSpeed);
        }
    }

    private void ClimbLadder()
    {
        if (!myColliderFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidBody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool(CLIMBING_KEY, false);
            return;
        }
        myRigidBody.gravityScale = 0f;
        float moveYPos = Input.GetAxis("Vertical") * climbSpeed;
        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, moveYPos);

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool(CLIMBING_KEY, playerHasVerticalSpeed);
    }

    private void Die()
    {
        if (myColliderBody.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger(DEATH_KEY);
            myRigidBody.velocity = deathKick;
            StartCoroutine(DelayBeforeProcessPlayerDeath(3f));
            
        }
    }

    IEnumerator DelayBeforeProcessPlayerDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}
