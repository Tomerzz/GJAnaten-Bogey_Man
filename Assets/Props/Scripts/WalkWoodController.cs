using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkWoodController : MonoBehaviour
{
    [Header("Sounds list")]
    public List<AudioSource> audioS = new List<AudioSource>();

    private float timer;

    [Header("Sound | ~~/100")]
    public float soundEmitted = 20.0f;


    void Start()
    {
        timer = 0.0f;
    }

    void Update()
    {
        if (timer > 0.0f) timer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<IPlayer>() && timer <= 0.0f)
        {
            IPlayer player = coll.gameObject.GetComponent<IPlayer>();

            player.DoingSound(soundEmitted);

            int random = Random.Range(0, 6);

            audioS[random].Play();
            timer = 1.0f;
        }
    }

}
