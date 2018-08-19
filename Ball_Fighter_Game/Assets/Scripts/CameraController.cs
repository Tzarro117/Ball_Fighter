using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    /*
    public GameObject player;
    public GameObject player2;

    private Vector3 offset;


    void Start()
    {
        offset = transform.position - player.transform.position;

    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
    */

    public Transform player1;
    public Transform player2;

    private const float DISTANCE_MARGIN = 5.0f;
    public float smoothing = 5f;

    private Vector3 middlePoint;
    private float distanceFromMiddlePoint;
    private float distanceBetweenPlayers;
    private float cameraDistance;
    private float aspectRatio;
    private float fov;
    private float tanFov;
    private float lowestPlayer;

    public float cameraTest;
    public float cameraTest2;

    void Start()
    {
        aspectRatio = Screen.width / Screen.height;
        tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
    }

    void FixedUpdate()
    {
        // Position the camera in the center.
        Vector3 newCameraPos = Camera.main.transform.position;
        newCameraPos.x = middlePoint.x;
        newCameraPos.z = middlePoint.z;
        Camera.main.transform.position = newCameraPos;

        // Find the middle point between players.
        Vector3 vectorBetweenPlayers = player2.position - player1.position;
        middlePoint = player1.position + 0.5f * vectorBetweenPlayers;
        // Added to move offset down.
        middlePoint.z -= 6;
        // Calculate the new distance.
        distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
        cameraDistance = (distanceBetweenPlayers / 2.0f /* better when changed from 2.0 to 1.0 */ / aspectRatio) / tanFov;
        // Clamp to stop zoom.
        cameraDistance = Mathf.Clamp(cameraDistance, 5, 1000);

        // Finds the "lowest" player position.
        lowestPlayer = Mathf.Min(player1.position.z, player2.position.z);

        // Set camera to new position.
        Vector3 dir = (Camera.main.transform.position - middlePoint).normalized;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
            (middlePoint + dir * (cameraDistance + DISTANCE_MARGIN)),
            smoothing * Time.deltaTime);
        // old version
        // Camera.main.transform.position = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN);


        if ((lowestPlayer - Camera.main.transform.position.z) <= 2.0f)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, (lowestPlayer - 2.0f));
        }

        // Output to AltMovement for testing.
        cameraTest = player1.position.z;
        cameraTest2 = Camera.main.transform.position.z;
    }
}
