using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject followObject;
    public float camSpeed;
    public float yPosition = 2.5f;
    public float xPosition = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 forward = followObject.transform.forward;
        Vector3 up = followObject.transform.up;

        Vector3 newPosition = new Vector3(followObject.transform.position.x + (forward.z * xPosition),
            followObject.transform.position.y + (up.y * yPosition), -10);

        //Refaire la speed de la cam en fonction de la speed du perso
        float moveSpeed = camSpeed * (newPosition - transform.position).magnitude;

        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.fixedDeltaTime);
    }
}
