using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class ExtraLife : MonoBehaviour {

	//Variables here

	Rigidbody2D rb;
	BoxCollider2D bc;


	// Use this for initialization	{
	void Start () {
		rb = GetComponent <Rigidbody2D> ();
		bc = GetComponent <BoxCollider2D> ();

		if(!rb)
		{
			rb = gameObject.AddComponent<Rigidbody2D>();
		}

		rb.gravityScale = 0;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;

		bc.isTrigger = true;
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D c)
	{

		if(c.gameObject.tag=="Player")
		{
////			Mario cc = c.gameObject.GetComponent<Mario>();
//			if(cc)
//			{
//				cc.lives++;
//			}
//


			Debug.LogError("Hit Player");
			Destroy(gameObject);
		}
	}
}
