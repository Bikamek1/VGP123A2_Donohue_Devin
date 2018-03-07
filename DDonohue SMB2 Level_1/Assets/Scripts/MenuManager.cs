using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public AudioSource sfx;
    public AudioClip playAudio;
    public AudioClip restartAudio;
    public AudioClip QuitAudio;
    public AudioClip uWinAudio;

    /*SceneManager.LoadScene("SMB2 Level_1");
    if (SceneManager.GetActiveScene().name == "MainMenu")
        SceneManager.LoadScene("SMB2 Level_1");
    else if (SceneManager.GetActiveScene().name == "MainMenu")
        ToGame();*/
    /*public void ToGame()

             SceneManager.LoadScene("SMB2 Level_1");
     }*/
    //this is my goto main menu function
    public void ToMainMenu()
    {
        
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Debug.Log("Am i working");
    }

    //
    public void ToGame()
    {
        this.sfx.PlayOneShot(this.playAudio);
        SceneManager.LoadScene("SMB2 Level_1", LoadSceneMode.Single);  
    }
    //This is my to game mode function

    //this is my continue level function
    public void ToContinue()
    {
        this.sfx.PlayOneShot(this.restartAudio);

        SceneManager.LoadScene("SMB2 Level_1", LoadSceneMode.Single);
        Debug.Log("Am i working");
    }
    //connect to dead character


    //this is my quit game mode function
    public void ToQuitGame()
    {
        this.sfx.PlayOneShot(this.QuitAudio);
        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Debug.Log("Am i working");
    }
    //connect to dead character and quitting the application

    public void UWin()
    {
        this.sfx.PlayOneShot(this.uWinAudio);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Debug.Log("Am i working");
    }



    void Update()
    {
       /*
        if (SceneManager.GetActiveScene().name == "SMB2 Level_1")
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        else if (SceneManager.GetActiveScene().name == "MainMenu")
            ToGame();
       */

     
    }

}




