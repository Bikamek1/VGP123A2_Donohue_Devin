using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public AudioSource sfx;
    public AudioClip pauseAudio;
    // Pause Stuff
    public bool paused = false;
    public GameObject MenuManager1;
    internal static object instance;

    // Pause Stuff

    // Pause Stuff
    void Start()
    {
        MenuManager1.SetActive(false);
    }
    // Pause Stuff

    // Pause Stuff

    void Pause()
    {
        Debug.Log("ITS WORKING"); //It was popping up but when i pressed P it stopped.. which means this is not running anymore.

        //Pause Stuff
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
        }

        if (paused)
        {
            this.sfx.PlayOneShot(this.pauseAudio);
            MenuManager1.SetActive(true);
            Time.timeScale = 0; //I tested to see if maybe "FixedUpdate" doesnt get called if time is stopped
            //The test was correct.. when time is stopped.. FixedUpdate is no longer called anywhere.
            //We can solve this by moving the logic to the "Update" method
        }

        else if (!paused)
        {
          //  this.sfx.PlayOneShot(this.pauseAudio);
            MenuManager1.SetActive(false);
            Time.timeScale = 1;
        }
        // Pause Stuff
    }


    //Fixed update runs for physics, at exactly 0.02 delay between updates (the number could be wrong.. might google it) And apparently changes depending on the TimeScale
    void FixedUpdate()
    {
       
    }


    //Update runs all the time (approximately 60-100 times per second (with a 0.017 more or less between updates)
    void Update()
    {
        Pause();
    }




}
