using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyController : MonoBehaviour
{

    private AudioSource audioS;
    private float timer;
    private SpriteRenderer spriteR;

    [Header("Sound | ~~/100")]
    public float soundEmitted = 10.0f;

    [Header("Sprites")]
    public Sprite sprite1;
    public Sprite sprite2;

    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        audioS = GetComponent<AudioSource>();
        timer = 0.0f;
    }

    void Update()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            spriteR.sprite = sprite2;
        }
        else spriteR.sprite = sprite1;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<IPlayer>() && timer <= 0.0f)
        {
            IPlayer player = coll.gameObject.GetComponent<IPlayer>();

            player.DoingSound(soundEmitted);

            audioS.Play();
            timer = 1.0f;
        }
    }

}
