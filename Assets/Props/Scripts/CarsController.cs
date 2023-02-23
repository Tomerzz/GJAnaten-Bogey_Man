using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsController : MonoBehaviour
{

    private AudioSource audioS;
    private float timer;
    private float movement = 1.0f;
    private Vector2 spawnTransform;
    //private Animator anim;
    private SpriteRenderer sprite;


    [Header("Speed & Lenght")]
    public float lenght = 3.0f;
    public float speed = 1.0f;

    [Header("Sound | ~~/100")]
    public float soundEmitted = 20.0f;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        //anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        timer = 0.0f;
        spawnTransform = transform.position;
    }

    void Update()
    {
        if (timer > 0.0f) timer -= Time.deltaTime;

        if (transform.position.y > spawnTransform.y + lenght)
        {
            sprite.flipY = true;
            movement = -1.0f;
        }
        if (transform.position.y < spawnTransform.y - lenght)
        {
            movement = 1.0f;
            sprite.flipY = false;
        }



        if (!audioS.isPlaying)
        {

            transform.position += new Vector3(0.0f, movement * speed, 0.0f) * Time.deltaTime;
            //anim.SetBool("move", false);

        }
        //else anim.SetBool("move", true);



    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<IPlayer>() && timer <= 0.0f)
        {
            IPlayer player = coll.gameObject.GetComponent<IPlayer>();

            if (!player.IsHiding)
            {
                player.DoingSound(soundEmitted);

                audioS.Play();
                timer = 4.0f;
            }

        }
    }
}
