using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ZoomIn : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject subCamera;
    public GameObject warpButton, sellectButton;
    public MapGenerator mapGenerator;
    public GameObject modeText;
    public GameObject reloadButton;

    private CollisionMap collisionMap;
    private GameObject arrow;
    private float vel = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (mapGenerator.map != null)
        {
            if (collisionMap == null)
            {
                Debug.Log("collisionMap is Setted");
                collisionMap = mapGenerator.map.GetComponent<CollisionMap>();
            }
            else if(collisionMap.arrowHit != null && arCamera.activeSelf)
            {
                Debug.Log("Camera is changed");
                arrow = collisionMap.arrowHit;
                //   Debug.Log(dispText);
                subCamera.transform.position = collisionMap.zoomCamera;
                arCamera.SetActive(false);
                subCamera.SetActive(true);
                reloadButton.SetActive(false);
            }
            else if(subCamera.activeSelf)
            {
                float distance = Vector3.Distance(subCamera.transform.position, arrow.transform.position);
               // Debug.Log(distance);
                if (distance > 0.6f)
                {
                    //Debug.Log("Camera is reached");
                    subCamera.transform.LookAt(arrow.transform);
                    subCamera.transform.Translate(0, 0, vel);
                    vel /= 1.05f;
                }
                else
                {
                    //Debug.Log("Camera is stopped");
                    collisionMap.arrowHit = null;
                    vel = 0.2f;
                    modeText.GetComponent<TextMeshProUGUI>().text = "地名をタップしてください";
                    warpButton.SetActive(true);
                    sellectButton.SetActive(true);
                    this.GetComponent<ArrowGenerator>().enabled = false;
                }           
            }
        }
    }
}
