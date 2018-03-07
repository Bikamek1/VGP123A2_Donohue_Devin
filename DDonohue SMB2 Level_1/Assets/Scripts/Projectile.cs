using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour{
    [SerializeField]
    // Used to tell GameObject (Projectile) how fast to move
    public float speed;

    // Used to tell GameObject (Projectile) how long to live without colliding with anything
    public float lifeTime;

    //Declares Rigidbody2D
    private Rigidbody2D myRigidbody;

    // Use this for initialization

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    
        // Check if speed was set to something not 0
        if (speed <= 0)
        {
            // Assign a default value if one is not set in the Inspector
            speed = 3.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.Log("speed was not set. Defaulting to " + speed);
        }

        // Check if speed was set to something not 0
        if (lifeTime <= 0)
        {
            // Assign a default value if one is not set in the Inspector
            lifeTime = 1.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.Log("lifeTime was not set. Defaulting to " + lifeTime);
        }

        // Take Rigidbody2D component and change its velocity to value passed
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

        /*
         * temp.GetComponent<Rigidbody2D>().velocity =
         * Vector2.right * projectileForce;
         */

        // Destroy gameObject after 'lifeTime' seconds
        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        myRigidbody.velocity = -transform.right * speed;
    }
}
