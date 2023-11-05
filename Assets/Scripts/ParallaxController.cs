using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public Transform[] parallaxLayers; // Tableau des couches de parallaxe.
    public float parallaxFactorHorizontal = 1f; // Facteur de parallaxe, plus il est élevé, plus le déplacement est important.
    public float parallaxFactorVertical = 1f; // Facteur de parallaxe, plus il est élevé, plus le déplacement est important.
    public float smoothing = 1f; // Lissage du mouvement.

    private Vector3 previousCameraPosition;

    void Start()
    {
        previousCameraPosition = Camera.main.transform.position;
    }

    void FixedUpdate()
    {
        Vector3 cameraMovement = previousCameraPosition - Camera.main.transform.position;

        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            float parallaxX = (cameraMovement.x * (1 - parallaxFactorHorizontal * (i + 1)));
            float targetX = parallaxLayers[i].position.x + parallaxX;
            float parallaxY = (cameraMovement.y * (1 - parallaxFactorVertical * (i + 1)));
            float targetY = parallaxLayers[i].position.y + parallaxY;

            Vector3 newPosition = new Vector3(targetX, targetY, parallaxLayers[i].position.z);
            parallaxLayers[i].position = Vector3.Lerp(parallaxLayers[i].position, newPosition, smoothing * Time.deltaTime);
        }

        previousCameraPosition = Camera.main.transform.position;
    }
}