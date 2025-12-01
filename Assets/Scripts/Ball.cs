using UnityEditor.Callbacks;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public float launchForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!GameManager.instance.isGameStarted)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                this.transform.SetParent(player.transform);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            this.transform.SetParent(null);
            GameManager.instance.isGameStarted = true;

            rb.AddForce(transform.up * launchForce, ForceMode.Impulse);
        }
    }
}
