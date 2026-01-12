using NUnit.Framework;
using UnityEngine;

public class BallArea : MonoBehaviour
{
    Ball ball;
    public float radio = 0.5f;
    public bool KeepInArea = false;

    public bool isInside;
    public bool isOutsideRight;
    public bool isOutsideLeft;
    public bool isOutsideTop;
    public bool isOutsideBottom;

    public float areaWidth;
    public float areaHeight;

    void Start()
    {
        areaHeight = Camera.main.orthographicSize;
        areaWidth = Camera.main.aspect * areaHeight;

    }

    void FixedUpdate()
    {
        Vector3 ballPos = ball.GetComponent<Transform>().position;
        isInside = true;
        isOutsideLeft = isOutsideRight = isOutsideTop = isOutsideBottom = false;

        if (ballPos.x > areaWidth - radio)
        {
            isOutsideRight = true;
            isInside = false;
        }
        if (ballPos.x < -areaWidth + radio)
        {
            isOutsideLeft = true;
            isInside = false;
        }
        if (ballPos.y > areaHeight - radio)
        {
            isOutsideTop = true;
            isInside = false;
        }
        if (ballPos.y < -areaHeight + radio)
        {
            isOutsideBottom = true;
            isInside = false;
        }

        isInside = !(isOutsideRight || isOutsideLeft || isOutsideTop || isOutsideBottom);
        if (KeepInArea && !isInside)
        {
            isInside = true;
            Invoke("ResetKeepInArea", 0.1f);
        }
    }

    public void SetBallInScene(GameObject ballGO)
    {
        ball = ballGO.GetComponent<Ball>();
    }

    void ResetKeepInArea()
    {
        KeepInArea = false;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 BorderSize = new Vector3(areaWidth * 2, areaHeight * 2, 0);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, BorderSize);
    }

}
