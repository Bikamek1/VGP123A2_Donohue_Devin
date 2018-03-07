using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathArea : MonoBehaviour {

    public AudioSource sfx;
    public AudioClip mDeathAudio;
    void OnTriggerEnter2D(Collider2D collision)
    {
        //game over screen
     if(collision.tag == "Player")
        {

            this.sfx.PlayOneShot(this.mDeathAudio);
            
            SceneManager.LoadScene("ContinueGameOver");
        }
     //Game over screen

     //Quit Game
        if (collision.tag == "Player")
        {
            this.sfx.PlayOneShot(this.mDeathAudio);
            Application.Quit();
        }
    }
    //Quit Game



}
