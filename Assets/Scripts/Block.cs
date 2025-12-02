using UnityEngine;
using UnityEngine.Video;

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

    public void OnHit(int damage)
    {
        vida -= damage * (100 / (100 + resistencia));
    }

    public virtual void BreakBlock()
    {
        GameManager.AddPoints(puntos);
        Destroy(this.gameObject);
    }
}