using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MotherController : MonoBehaviour
{
    private float timer;
    private float timerForMotherCome;
    IPlayer player;
    private bool motherCheck;
    private bool motherCome;
    private bool lightActive;

    [Header("Audio")]
    public List<AudioSource> audioS = new List<AudioSource>();

    [Header("Light (on/off)")]
    public Light2D light2D;


    void Start()
    {
        motherCheck = false;
        motherCome = false;

        player = FindObjectOfType<IPlayer>();

        timer = 0.0f;
        timerForMotherCome = 0.0f;

    }



    void Update()
    {


        if (timer > 0.0f) timer -= Time.deltaTime;
        if (timerForMotherCome > 0.0f) timerForMotherCome -= Time.deltaTime;




        float random = Random.Range(70.0f, 100.0f);

        if (player.CurrentSound >= random)
        {
            if (motherCome == false)
            {
                timerForMotherCome = 6.0f;
                audioS[2].Play();
                motherCome = true;
            }
        }



        if (lightActive)
        {
            if(light2D.intensity <= 0.5f) light2D.intensity += Time.deltaTime;
            if (!player.IsHiding) player.GetCought = true;

        } else if (!lightActive && light2D.intensity >= 0.0f) light2D.intensity -= Time.deltaTime;





        if (motherCome && timerForMotherCome <= 0.0f) MotherOpenDoor();
        if (motherCheck && timer <= 0.0f) MotherCloseDoor();

    }




    private void MotherOpenDoor()
    {
        audioS[0].Play();
        motherCheck = true;
        motherCome = false;
        timer = 5.0f;
        player.CurrentSound = 0.0f;

        lightActive = true;

    }

    private void MotherCloseDoor()
    {
        motherCheck = false;
        audioS[1].Play();

        lightActive = false;

    }

}
