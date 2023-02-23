using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    Animator anim;
    IPlayer player;

    bool canOpen = false;

    public GameObject light;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && canOpen)
        {
            anim.SetTrigger("open");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<IPlayer>();

            Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);
            player.PanelInteract.transform.position = pos;
            player.TxtInteract.text = "<e> ouvrir";
            player.PanelInteract.SetActive(true);

            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.PanelInteract.SetActive(false);
            canOpen = false;
        }
    }

    void StartLevel()
    {
        light.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }
}
