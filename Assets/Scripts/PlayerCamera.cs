using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public Transform player;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Rigidbody playersRb;
    private Vector3 playerVelocity;

    private void Start()
    {
        playersRb = player.gameObject.GetComponent<Rigidbody>();
    }

    //You need to use whatever update the players movement is being calculated in
    private void FixedUpdate()
    {
        playerVelocity = playersRb.velocity;
        Vector3 toPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, toPosition, ref playerVelocity, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(player);
    }

}
