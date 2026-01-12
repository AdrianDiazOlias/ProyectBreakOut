using UnityEngine;

public class Block : MonoBehaviour
{
    public float vida;
    public float resistencia;
    public float puntos;

    void Update()
    {
        if (vida <= 0)
        {
            BreakBlock();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            Bounce(collision);
            OnHit(ball.damage);
        }
    }

    public void OnHit(float damage)
    {
        vida -= damage * (100 / (100 + resistencia));
    }

    public virtual void BreakBlock()
    {
        GameManager.AddPoints(puntos);
        Debug.Log($"{this.gameObject.name} destroyed!");
        Destroy(this.gameObject);
    }

    void Bounce(Collision collision)
    {
        Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 direccion = collision.contacts[0].point - transform.position;
        direccion = direccion.normalized;
        ballRb.linearVelocity = direccion * collision.gameObject.GetComponent<Ball>().ballSpeed;
    }
}