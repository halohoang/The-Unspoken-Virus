using UnityEngine;
using System.Collections;

public class Parallex : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    // Use this for initialization
    void Start()
    {
        cameraTransform = Camera.main.transform;

            }

    private void Latepdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        float parallaxEffectMultipiler = 0.5f;
        transform.position += deltaMovement;
        lastCameraPosition = cameraTransform.position;
    }

}
