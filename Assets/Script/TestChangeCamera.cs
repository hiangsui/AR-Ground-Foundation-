using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TestChangeCamera : MonoBehaviour
{
    public ARCameraManager arCamManager;

    public void ButtonChangeCam()
    {
        if (arCamManager.requestedFacingDirection == CameraFacingDirection.World)
        {
            arCamManager.requestedFacingDirection = CameraFacingDirection.User;
        }

        else
        {
            arCamManager.requestedFacingDirection = CameraFacingDirection.World;
        }

        Debug.Log(arCamManager.requestedFacingDirection);
    }
}
