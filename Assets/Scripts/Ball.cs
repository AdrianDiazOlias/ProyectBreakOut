using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    BallArea ballArea;
    GameObject player;
    Vector3 lastPos;

    [Header("Ball Stats")]
    public float ballSpeed = 5f;
    public float damage = 10;

    void Start()
    {
        gameObject.name = "Ball";
        rb = GetComponent<Rigidbody>();
        ballArea = Camera.main.GetComponent<BallArea>();
        ballArea.SetBallInScene(this.gameObject);
        player = GameObject.FindGameObjectWithTag("Player");

        ResetBallIntoPlayer();

        AdjustDificultyValues(ScreenManager.instance.dificultyDropdown.value);
        ScreenManager.instance.dificultyDropdown.onValueChanged.AddListener(
            delegate
            {
                AdjustDificultyValues(ScreenManager.instance.dificultyDropdown.value);
            });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.instance.isGameStarted)
        {
            this.transform.SetParent(null);
            GameManager.instance.isGameStarted = true;

            rb.linearVelocity = transform.up * ballSpeed;
        }

        Vector3 incoming = rb.linearVelocity;

        if (incoming.sqrMagnitude < 0.0001f)
        {
            lastPos = transform.position;
            return;
        }

        if (ballArea.isOutsideBottom)
        {
            GameManager.instance.PlayerDamaged();
            ResetBallIntoPlayer();
            ballArea.isOutsideBottom = false;
        }


        if (ballArea.isOutsideTop)
        {
            Debug.Log("Top Bounce");
            Vector3 normal = Vector3.down;
            Vector3 reflected = Vector3.Reflect(incoming.normalized, normal) * ballSpeed;
            rb.linearVelocity = reflected;
            ballArea.isOutsideTop = false;
        }

        if (ballArea.isOutsideLeft)
        {
            Debug.Log("Left Bounce");
            Vector3 normal = Vector3.right;
            Vector3 reflected = Vector3.Reflect(incoming.normalized, normal) * ballSpeed;
            rb.linearVelocity = reflected;
            ballArea.isOutsideLeft = false;
        }

        if (ballArea.isOutsideRight)
        {
            Debug.Log("Right Bounce");
            Vector3 normal = Vector3.left;
            Vector3 reflected = Vector3.Reflect(incoming.normalized, normal) * ballSpeed;
            rb.linearVelocity = reflected;
            ballArea.isOutsideRight = false;
        }

    }
    void FixedUpdate()
    {
        lastPos = transform.position;
    }

    public void ResetBallIntoPlayer()
    {
        if (player != null)
        {
            GameManager.instance.isGameStarted = false;
            rb.linearVelocity = Vector3.zero;
            this.transform.SetParent(player.transform);
            this.transform.localPosition = new Vector3(0, 4, 0);
        }
    }

    public void AdjustDificultyValues(int Dificulty)
    {
        switch (Dificulty)
        {
            case 0:
                damage = 20f;
                ballSpeed = 5f;
                break;
            case 1:
                damage = 10f;
                ballSpeed = 7.5f;
                break;
            case 2:
                damage = 5f;
                ballSpeed = 10f;
                break;
        }
    }
}
