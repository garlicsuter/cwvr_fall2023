using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourVRHandScript : MonoBehaviour
{
  public float GetHandRotation()
    {
        float handRot = transform.localEulerAngles.y;
        return handRot;
        Debug.Log("handRot returned: " + handRot);
    }
}
