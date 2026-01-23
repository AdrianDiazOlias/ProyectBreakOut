using NUnit.Framework;
using UnityEngine;

public class BallArea : MonoBehaviour
{
    Ball ball;
    Rigidbody ballRb;
    public float ballRadio = 0.5f;

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
        Vector3 ballPos = ball.transform.position;
        Vector3 ballSpeed = ballRb.linearVelocity;


        isOutsideLeft = isOutsideRight = isOutsideTop = isOutsideBottom = false;
        if (ballPos.x > areaWidth - ballRadio && ballSpeed.x > 0)
        {
            isOutsideRight = true;
        }
        if (ballPos.x < -areaWidth + ballRadio && ballSpeed.x < 0)
        {
            isOutsideLeft = true;
        }
        if (ballPos.y > areaHeight - ballRadio && ballSpeed.y > 0)
        {
            isOutsideTop = true;
        }
        if (ballPos.y < -areaHeight + ballRadio && ballSpeed.y < 0)
        {
            isOutsideBottom = true;
        }
    }

    public void SetBallInScene(GameObject ballGO)
    {
        ball = ballGO.GetComponent<Ball>();
        ballRb = ballGO.GetComponent<Rigidbody>();
        ballRadio = ballRb.transform.localScale.x;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 BorderSize = new Vector3(areaWidth * 2, areaHeight * 2, 0);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, BorderSize);
    }

}
