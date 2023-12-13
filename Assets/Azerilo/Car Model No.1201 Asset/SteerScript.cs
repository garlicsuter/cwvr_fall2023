using UnityEngine;

public class SteerScript : MonoBehaviour
{
    private bool isGrabbed = false;
    private Transform hand;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isGrabbed = true;
            hand = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isGrabbed = false;
            hand = null;
        }
    }

    private void Update()
    {
        if (isGrabbed)
        {
            // Rotate the steering wheel based on hand movement
            float rotationAmount = hand.GetComponent<YourVRHandScript>().GetHandRotation(); // Implement this method in your VR hand script
            transform.Rotate(Vector3.up, rotationAmount);
        }
    }
}
