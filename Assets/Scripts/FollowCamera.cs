using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Project
{
    public class FollowCamera : MonoBehaviour
    {
        // Variables
        public Transform target;

        //how quickly the camera snap to the player
        public float smoothSpeed = 10f;

        //Create offset so the camera is not "inside" of the player
        public Vector3 offset;



        // Functions
        //Don't ask me why i replace LateUpdate with FixedUpdate, it's worked 
        void FixedUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
