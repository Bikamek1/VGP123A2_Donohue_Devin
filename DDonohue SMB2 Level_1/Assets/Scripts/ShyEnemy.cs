using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShyEnemy : MonoBehaviour{
    public bool isFacingRight;

    void OnCollisionEnter2D(Collision2D coll)
    {
    //change direction
        if (coll.gameObject.tag == "EnemyCol")// checking if it was collided with      
        {
           
            ChangeDirection();
            EnemySpeed *= -1;
        }

        // Change Direction
    }

    void ChangeDirection()
            {
                //define the class here
                isFacingRight = !isFacingRight;

                Vector2 scaleFactor = transform.localScale;

                // Flip scale of 'x' variable
                scaleFactor.x *= -1;    // scaleFactor.x = -scaleFactor.x;

                // Update scale to new flipped value
                transform.localScale = scaleFactor;
            }

    //Movement Function Here
    public int EnemySpeed;
	public int XMoveDirection;
    // Update is called once per frame
    void Update ()
    {
      
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (XMoveDirection, 0) * EnemySpeed;
     
    }
  
}