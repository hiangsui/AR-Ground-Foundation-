using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObject : MonoBehaviour
{
    ARRaycastManager arRaycastManager;
    Pose pose;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject marker;
    public GameObject prefabModel;

    GameObject newModel;

    bool placeObject = false;

    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        marker.SetActive(false);
    }

    void Update()
    {
        CheckPlatform();
    }

    void CheckPlatform()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            DetectSurface();
        }

        else if (Application.isEditor)
        {

        }
    }

    public void ButtonPlaceObject()
    {
        InstantModel();
        placeObject = !placeObject;

        if (placeObject == false)
        {
            Debug.Log("Unlock");
        }

        else
        {
            Debug.Log("Lock");
        }
    }

    void InstantModel()
    {
        // ถ้ายังไม่ได้ Instantiate หรือ ยังไม่มี Model //

        if (newModel == null)
        {
            newModel = Instantiate(prefabModel, pose.position, pose.rotation);

            Debug.Log("Instant Model");
        }
    }

    void DetectSurface()
    {
        // กำหนดให้ screenCenter เท่ากับตำแหน่งของ Camera //

        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        // ถ้า Detect เจอพื้น //

        if (arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            pose = hits[0].pose;
            marker.SetActive(true);
            marker.transform.SetPositionAndRotation(pose.position, pose.rotation);

            //Debug.Log("Detect Surface");

            // ถ้ายังไม่ได้วาง GameObject //

            if (placeObject == false && newModel != null)
            {
                newModel.transform.SetPositionAndRotation(pose.position, pose.rotation);
            }
        }

        else
        {
            marker.SetActive(false);

            //Debug.Log("Not Detect");
        }
    }
}
