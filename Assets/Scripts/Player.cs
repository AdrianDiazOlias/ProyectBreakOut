using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float sideMoveSpeed;
    public float maxSideMove;

    Vector3 mousePos2D;
    Vector3 mousePos3D;

    void Start()
    {
        Vector3 playerPos = this.transform.position;

    }

    void Update()
    {
        // mousePos2D = Input.mousePosition;
        // mousePos2D.z = -Camera.main.transform.position.z;
        // mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 Pos = this.transform.position;
        // Pos.x = mousePos3D.x;

        Pos.x += Input.GetAxis("Horizontal") * sideMoveSpeed * Time.deltaTime;

        if (Pos.x > maxSideMove)
        {
            Pos.x = maxSideMove;
        }
        else if (Pos.x < -maxSideMove)
        {
            Pos.x = -maxSideMove;
        }
        this.transform.position = Pos;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Bounce(collision);
        }
    }

    void Bounce(Collision collision)
    {
        Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 direccion = collision.contacts[0].point - transform.position;
        direccion = direccion.normalized;
        ballRb.linearVelocity = direccion * collision.gameObject.GetComponent<Ball>().ballSpeed;
    }
}
