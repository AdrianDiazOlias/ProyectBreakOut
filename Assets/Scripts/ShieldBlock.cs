using UnityEngine;

public class ShieldBlock : Block
{
    bool shieldActive = true;

    public override void BreakBlock()
    {
        if (shieldActive)
        {
            shieldActive = false;
            Renderer rend = GetComponent<Renderer>();
            rend.material.color = Color.blue;
        }
        else
        {
            AddPoints(puntos);
            GameManager.instance.shieldBuffActive = true;
            Destroy(this.gameObject);
        }
    }
}
