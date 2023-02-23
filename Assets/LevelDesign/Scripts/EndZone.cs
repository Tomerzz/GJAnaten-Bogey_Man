using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public AudioSource bgSound;
    public float timeToReduce = 1.5f;

    float maxVolume;
    bool soundReduce = false;

    // Start is called before the first frame update
    void Start()
    {
        maxVolume = bgSound.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (soundReduce) bgSound.volume -= Time.deltaTime * timeToReduce;
        if (!soundReduce && bgSound.volume != maxVolume) bgSound.volume += Time.deltaTime * timeToReduce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        soundReduce = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        soundReduce = false;
    }
}
