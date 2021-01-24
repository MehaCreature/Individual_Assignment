using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource openingTune;
    public AudioSource backgroundMusic;
    //public AudioSource victoryMusic;
    //public AudioSource gameoverMusic;

    public int trackNum =1;

    private KnightController knightController;
    // Start is called before the first frame update
    void Start()
    {
        trackNum = 1;

        GameObject knightCont = GameObject.FindWithTag("Knight");
        if(knightCont !=null)
        {
            knightController = knightCont.GetComponent<KnightController>();
            print("Knight Controller is working");
        }
        if(knightCont == null)
        {
            print("Knight Controller not working");
        }

        openingTune.Play();

        //victoryMusic.Stop();

        //gameoverMusic.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        AudioManager();
    }

    void AudioManager()
    {
        if(knightController.endstartTune == true && trackNum !=2)
        {
            trackNum = 2;

            openingTune.Stop();

            backgroundMusic.Play();
        }

       if(knightController.victory == true && trackNum !=3)
        {
           trackNum = 3;

           backgroundMusic.Stop();

           //victoryMusic.Play();
        }

        if(knightController.gameOver == true && trackNum !=4)
        {
            trackNum = 4;

            backgroundMusic.Stop();

            //gameoverMusic.Play();
        }
    }
}
