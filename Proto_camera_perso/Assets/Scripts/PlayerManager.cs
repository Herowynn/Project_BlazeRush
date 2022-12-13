using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float speed;
    public float maxSpeed;


    private float player2VerticalAxis = 0f;
    private float player2HorizontalAxis = 0f;

    void Update()
    {
/*        Debug.Log("Vitesse joueur 1 : " + player1.GetComponent<Rigidbody>().velocity.magnitude);*/

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0) 
        {
            player1.GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxis("Vertical") * 100 * speed, 0f, -Input.GetAxis("Horizontal") * 100 * speed));
            player1.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(player1.GetComponent<Rigidbody>().velocity, maxSpeed);
        }

        if (Input.GetKey("z"))
        {
            player2VerticalAxis = 1;
        }
        else if (Input.GetKey("s"))
        {
            player2VerticalAxis = -1;
        }
        else
        {
            player2VerticalAxis = 0;
        }

        if (Input.GetKey("q"))
        {
            player2HorizontalAxis = 1;
        }
        else if (Input.GetKey("d"))
        {
            player2HorizontalAxis = -1;
        }
        else
        {
            player2HorizontalAxis = 0;
        }

        if (Mathf.Abs(player2HorizontalAxis) > 0 || Mathf.Abs(player2VerticalAxis) > 0) 
        {
            player2.GetComponent<Rigidbody>().AddForce(new Vector3(player2VerticalAxis * 100 * speed, 0f, player2HorizontalAxis * 100 * speed));
            player2.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(player2.GetComponent<Rigidbody>().velocity, maxSpeed);
        }
    }
}
