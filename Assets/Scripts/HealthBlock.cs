using UnityEngine;

public class HealthBlock : Block
{
    public override void BreakBlock()
    {
        GameManager.instance.PlayerHealed();
        GameManager.AddPoints(puntos);
        Destroy(this.gameObject);
    }
}