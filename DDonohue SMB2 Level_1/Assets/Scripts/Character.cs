using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Character : MonoBehaviour
{
    //Player HP
    public int currentHealth = 2;
    public int maxHealth = 2;
    //Player HP

    //Gain HP
    public int Grow = +1;
    //Gain HP

    //Sound
    public AudioSource sfx;
    public AudioClip MDeathAudio;
    public AudioClip jumpAudio;
    public AudioClip CollectAudio;
    public AudioClip eDamageAudio;
    public AudioClip pickUpAudio;

    public AudioClip shrinkAudio;

    public AudioClip growSndAudio;
    public AudioSource LevelMusicAudio;
    public int Instance;
    //Sound

    //Score variables
    private int Count;
    public Text countText;
    //Score variables

    // Enemies Killed
    private int KillCount;
    public Text killText;
    // Enemies Killed


    //Attack variables
    public Transform jumpAttack;
    //Attack variables

    //HealthHud
    public HealthHUD healthHUD;
    //HealthHud

    // Method 1: Reference Rigidbody2D through script
    // - Not shown in Inspector
    Rigidbody2D rb;

    // Method 2: Reference Rigidbody2D through script
    // - Shown in Inspector
    public Rigidbody2D rb2;

    // Handle movement speed of Character
    // - Can be adjusted through Inspector while in Play mode
    public float speed;
    public float moveValue;

    // Handles jump speed of Character
    public float jumpForce;             // How high the character will Jump
    public bool isGrounded;             // Is the player touching the ground?
    public LayerMask isGroundLayer;     // What is the Ground? Player can only jump on things that are on the "Ground" layer  
    public Transform groundCheck;       // Used to figure out if the player is touching the ground
    public float groundCheckRadius;     // Size of circle around empty GameObject

    // Handles animations
    public Animator Anim;

    public AnimatorController SmallMario;
    public AnimatorController BigMario;



    // Handles Character flipping
    public bool isFacingRight;

    // Use this for initialization
    void Start()
    {
        //Player HP
        currentHealth = maxHealth;
        //Player HP


        //Here we declare are count and kill count to 0
        Count = 0;
        SetCountText ();

        KillCount = 0;
        SetKillText();
        //Here we declare are count and kill count to 0


        // Method 1: Reference Rigidbody through script
        rb = GetComponent<Rigidbody2D>();

        // Checks if Component exists
        if (!rb)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("No Rigidbody2D found.");
        }

        // Checks if Component exists
        if (!rb2)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            rb2 = GetComponent<Rigidbody2D>();
        }

        // Check if speed was set to something not 0
        if (speed <= 0)
        {
            // Assign a default value if one is not set in the Inspector
            speed = 5.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("Default speed to " + speed);
        }


        // Check if speed was set to something not 0
        if (jumpForce == 0)
        {
            // Assign a default value if one is not set in the Inspector
            jumpForce = 5.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.Log("Jump Force was not set. Defaulting to " + jumpForce);
        }

        // Checks if GroundCheck GameObject is connected
        if (!groundCheck)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("No Ground Check found on GameObject");
        }

        // Check if speed was set to something not 0
        if (groundCheckRadius == 0)
        {
            // Assign a default value if one is not set in the Inspector
            groundCheckRadius = 0.2f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.Log("Ground Check Radius was not set. Defaulting to " + groundCheckRadius);
        }

        // Reference Component through script
        Anim = GetComponent<Animator>();

        SmallMario = GetComponent<AnimatorController>();

        // Checks if Component exists
        if (!Anim)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("No Animator found on GameObject");
        }

        // Checks if Component exists
        if (!SmallMario)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("No Animator found on GameObject");
        }

        this.healthHUD.ChangeDisplayedHealth(this.currentHealth);

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Health: " + currentHealth);




        // Check if groundCheck GameObject is touching something tagged as Ground or Platform
        // - Can change groundCheckRadius to a smaller value if you need more precision or if the sizing of the Character is small
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,
            isGroundLayer);

        // Checks if Left (or a) or Right (or d) is pressed
        // - "Horizontal" must exist in Input Manager (Edit-->Project Settings-->Input)
        // - Returns -1(left), 1(right), 0(nothing)
        // - Use GetAxis for value -1-->0-->1 and all decimal places. (Gradual change in values)
        float moveValue = Input.GetAxisRaw("Horizontal");

        // Check if "Jump" button was pressed
        // - "Jump" must exist in Input Manager (Edit-->Project Settings-->Input)
        // - Configuration can be changed later
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.sfx.PlayOneShot(this.jumpAudio);

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.Log("Jump");

            // Vector2.up --> new Vector2(0,1);
            // Vector2.down --> new Vector2(0,-1);
            // Vector2.right --> new Vector2(1,0);
            // Vector2.left --> new Vector2(-1,0);

            // Applies a force in the UP direction
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

     

        // Move Character using Rigidbody2D
        // - Uses moveValue from GetAxis to move left or right
        rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);

        // Tells Animator to transition to another Clip
        // - Parameter must be created in Animator window
        Anim.SetFloat("Speed", Mathf.Abs(moveValue));
        Anim.SetBool("Jump", !isGrounded);


        //This checks if Character should flip left and right
        if ((!isFacingRight && moveValue < 0) || (isFacingRight && moveValue > 0))
            // Call function to flip Character
            Flip();
        //This checks if Character should flip left and right


        
        //Player HP
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        //Player HP

        //Death
        if (currentHealth <= 0)
        {
            Die();
          
        }
        //Death
      
    }

    //Player loses HP
    void Hit()
    {
        
        //SmallMario.SetBool("Shrink", true);
        //mario damage audio
        this.sfx.PlayOneShot(this.shrinkAudio);
        //mario damage audio

        maxHealth--;
    }
    //Player loses HP

    // Die Function

    void Die()
    {
       
        this.sfx.PlayOneShot(this.MDeathAudio);
        //fix die audio
        SceneManager.LoadScene("ContinueGameOver", LoadSceneMode.Single);
        // Awesome sound clip
       
       

    }
    // Die Function

    // Function used to flip direction GameObject (Character) is facing
    void Flip()
    {
        // Method 1: Toggle isFacingRight variable
        //isFacingRight = !isFacingRight;

        // Method 2: Toggle isFacingRight variable
        
        if (isFacingRight)
            isFacingRight = false;
        else
            isFacingRight = true;
        

        // Make a copy of old scale value
        Vector3 scaleFactor = transform.localScale;

        // Flip scale of 'x' variable
        scaleFactor.x *= -1;    // scaleFactor.x = -scaleFactor.x;

        // Update scale to new flipped value
        transform.localScale = scaleFactor;
        
    }

    //Collision Detection Here
    void OnCollisionEnter2D(Collision2D collision)
    {
  
        //PowerUP
        if (collision.gameObject.tag == "PowerUp")
            Debug.Log("I hit a PowerUp");
        //PowerUP

        //Collectible
        else if (collision.gameObject.tag == "Collectible")
            Debug.Log("I hit a Collectible");
        //Collectible

        //Shy Enemy
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("I hit a Shy Enemy");
            if (collision.gameObject.transform.position.y < jumpAttack.position.y)
            {
                this.sfx.PlayOneShot(this.eDamageAudio);
                //shy AUDIO
                Debug.Log("I jump attacked Shy Enemy");
                Destroy(collision.gameObject);
                KillCount = KillCount + 1;
                SetKillText();
            }
            else
            {
                this.sfx.PlayOneShot(this.shrinkAudio);
                currentHealth--;
                this.healthHUD.ChangeDisplayedHealth(this.currentHealth);
            }
        

        }
        //Shy Enemy

        //Snake Projectile
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("I hit a Shy Enemy");
            if (collision.gameObject.transform.position.y < jumpAttack.position.y)
            {
                
                Debug.Log("I jump attacked Shy Enemy");
                Destroy(collision.gameObject);
                KillCount = KillCount + 1;
                SetKillText();
            }
            else
                this.sfx.PlayOneShot(this.shrinkAudio);
            currentHealth--;
            this.healthHUD.ChangeDisplayedHealth(this.currentHealth);
        }
        //Snake Projectile

        //Snake Enemy
        if (collision.gameObject.tag == "SnakeEnemy")
        {
            Debug.Log("I hit a Snake Enemy");
            if (collision.gameObject.transform.position.y < jumpAttack.position.y)
            {
                this.sfx.PlayOneShot(this.eDamageAudio);
                //snake audio
                Debug.Log("I jump attacked Snake Enemy");
                Destroy(collision.gameObject);
                KillCount = KillCount + 1;
                SetKillText();
             
            }
            else
                currentHealth--;
            Debug.Log("Did i just auto die??????");
        }
        //Snake Enemy

        //Platform
        if (collision.gameObject.tag == "Platform")
            Debug.Log("I hit a Block/Platform");
        //Platform

        //SnakeEnemy
        if (collision.gameObject.tag == "SnakeEnemy")
            Debug.Log("I hit a SnakeEnemy");
        //SnakeEnemy

        //Obstacle
        if (collision.gameObject.tag == "Obstacle")
            Debug.Log("I hit a Obstacle");
        //Obstacle

        //EnemyCol
        if (collision.gameObject.tag == "EnemyCol")
            //Debug.Log("I hit a EnemyCol");
            //EnemyCol

            //EnemyCol
            if (collision.gameObject.tag == "Projectile")
            Debug.Log("I hit a Projectile1");
            //EnemyCol
    }
    //Collision Detection Here

    //Here we set are count text
    void SetCountText()
    {
        countText.text = "Count:" + Count.ToString();
    }
    //Here we set are count text

    //Here we set are kill text
    void SetKillText()
    {
        killText.text = "Kill:" + KillCount.ToString();
    }
    //Here we set are kill text

    //When Rigidbody collider collides with collider marked as trigger
    void OnTriggerEnter2D(Collider2D other)

    {
   
        //Collect Collectible
        if (other.gameObject.CompareTag("Collectible"))
        {
            //Awesome sound clip
            this.sfx.PlayOneShot(this.CollectAudio);

            other.gameObject.SetActive(false);
            Count = Count + 1;
            //countText.text = "Count:" + Count.ToString();
            SetCountText();

            this.sfx.PlayOneShot(this.CollectAudio);
        }
        //Collect Collectible

        //Collect PowerUp
        if (other.gameObject.CompareTag("PowerUp"))
        {
            //Awesome sound clip
            this.sfx.PlayOneShot(this.pickUpAudio);

            other.gameObject.SetActive(false);
            //Best one to use
            Debug.Log("am i playing");
            Debug.Log("I hit a PowerUp");

            //PowerUp Grow
          //public int Grow = +1;
            if (currentHealth < maxHealth)
            {
                if (currentHealth < Grow)
                {
                    //grow int is +1
                    // Anim.
                }

                //Grow sound
                this.sfx.PlayOneShot(this.growSndAudio);
                //Grow sound
                //Hp increase
                currentHealth = currentHealth + Grow;
                Debug.Log("Health Increased by 1");
                //Hp increase
            }
            //PowerUp Grow


            // Check if speed was set to something not 0
            if (Anim)
            {
                // My character controllers
                Anim.runtimeAnimatorController = SmallMario;
                Anim.runtimeAnimatorController = BigMario;

                // Prints a message to Console (Shortcut: Control+Shift+C)
                Debug.Log("Big Mario Referenced");
            }




            this.healthHUD.ChangeDisplayedHealth(this.currentHealth);

        }
        // Im in void OnTriggerEnter2D Area Right now

        //U Win
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene("UWin", LoadSceneMode.Single);
        }
        //U Win


    }
    //When Rigidbody collider collides with collider marked as trigger
    //End of Class
}
//End of class
