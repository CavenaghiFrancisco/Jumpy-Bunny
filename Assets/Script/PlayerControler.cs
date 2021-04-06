using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float thrust = 10.0f;
    public LayerMask groundLayerMask;
    public Animator animator;
    public float runSpeed = 3f;
    private static PlayerControler sharedInstance;
    private float initialGravity;

    private Vector3 initialPosition;
    private Vector2 initialVelocity;

    private void Awake()
    {

        sharedInstance = this;
        initialPosition = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        initialVelocity = rigidBody.velocity;
        animator.SetBool("isAlive", true);
        initialGravity = rigidBody.gravityScale;
    }

    public static PlayerControler GetInstance()
    {
        return sharedInstance;
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        animator.SetBool("isAlive", true);
        transform.position = initialPosition;
        rigidBody.velocity = initialVelocity;
        rigidBody.gravityScale = initialGravity;
    }

    // Update is called once per frame
    void Update()
    {
        bool canJump = GameManager.GetInstance().currentGameState == GameState.InGame;
        bool isOnTheGround = IsOnTheGround();
        animator.SetBool("isGrounded", isOnTheGround);
        if (canJump && Input.GetKeyDown(KeyCode.Space) && isOnTheGround)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        GameState currState = GameManager.GetInstance().currentGameState;
        if (currState == GameState.InGame) 
        {
            if (rigidBody.velocity.x < runSpeed)
            {
                rigidBody.velocity = new Vector2(runSpeed, rigidBody.velocity.y);
            }
        }
        
    }

    void Jump()
    {
        rigidBody.AddForce(Vector2.up * thrust, ForceMode2D.Impulse);
        rigidBody.AddForce(Vector2.right * thrust, ForceMode2D.Force);
    }

    bool IsOnTheGround()
    {
        return Physics2D.Raycast(rigidBody.transform.position, Vector2.down, 1f, groundLayerMask.value);
    }

    public void KillPlayer()
    {
        animator.SetBool("isAlive", false);
        int highestScore = PlayerPrefs.GetInt("highestScore");
        int currentScore = GetDistance();
        if(currentScore > highestScore)
        {
            PlayerPrefs.SetInt("highestScore", currentScore);
        }
        rigidBody.gravityScale = 0f;
        rigidBody.velocity = Vector2.zero;
        
        GameManager.GetInstance().GameOver();
        
    }

    public int GetDistance()
    {
        var distance = (int)Vector2.Distance(initialPosition, transform.position);

        return distance;
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("highestScore");
    }

}
