using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{
    IPlayer player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<IPlayer>();

            Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);
            player.PanelInteract.transform.position = pos;
            player.TxtInteract.text = "<e> se cacher";
            player.PanelInteract.SetActive(true);

            player.CanHide = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.IsHiding)
            {
                player.TxtInteract.text = "<e> sortir";

                Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);
                player.PanelInteract.transform.position = pos;
            }
            else if (!player.IsHiding)
            {
                player.TxtInteract.text = "<e> se cacher";

                Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);
                player.PanelInteract.transform.position = pos;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.PanelInteract.SetActive(false);

            player = collision.gameObject.GetComponent<IPlayer>();
            player.CanHide = false;
        }
    }
}
