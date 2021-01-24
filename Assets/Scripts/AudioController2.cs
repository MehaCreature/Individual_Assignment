using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController2 : MonoBehaviour
{
    //public AudioSource victoryMusic;
    public AudioSource gameoverMusic;
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
    }

    // Update is called once per frame
    void Update()
    {
        AudioController();
    }

    void AudioController()
    {
       // if(knightController.victory == true && trackNum !=3)
        //{
          // trackNum = 3;

           //victoryMusic.Play();
        //}

        if(knightController.startgameOver == true && trackNum !=4)
        {
            trackNum = 4;

            gameoverMusic.Play();
        }
    }
}
