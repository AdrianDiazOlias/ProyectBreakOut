using UnityEngine;

public class GoldBlock : Block
{
    public float puntosBonus = 10f;

    void Start()
    {
        this.puntos = puntos + puntosBonus;
    }

    public override void BreakBlock()
    {
        GameManager.AddPoints(puntos);
        GameManager.instance.goldBuffActive = true;
        Destroy(this.gameObject);
    }
}