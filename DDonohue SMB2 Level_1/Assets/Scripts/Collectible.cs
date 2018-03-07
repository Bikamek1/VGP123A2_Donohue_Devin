using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // press Control and ' to bring up the unity help menu
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

    }
    void OnTriggerEnter2D(Collider2D other)

    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
        }
    }
    //End of Class Function
}
//End of Class Function