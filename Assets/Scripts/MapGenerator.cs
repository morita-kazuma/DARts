using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class MapGenerator : MonoBehaviour
{    
    public GameObject objectPrefab;
    public TrackableType type;

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    private ARPlaneManager planeManager;

    [HideInInspector] public bool isGenerated = false;
    [HideInInspector] public GameObject map;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGenerated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (raycastManager.Raycast(Input.GetTouch(0).position, hitResults, type))
                {
                    map = Instantiate(objectPrefab, hitResults[0].pose.position, hitResults[0].pose.rotation * Quaternion.Euler(90, 0, 0));
                    isGenerated = true;
                    foreach(var plane in planeManager.trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }
                    planeManager.planePrefab = null;
                }
            }
        }
    }
}
