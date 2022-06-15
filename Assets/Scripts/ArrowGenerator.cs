using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject arCameraObject;

    private List<GameObject> ArrowList = new List<GameObject>();
    private Vector3 ReleasePos;
    private Vector3[] TouchPosList;
    private int count;
    private bool isReady;

    [HideInInspector] public bool arrowGenerated=false;
    public MapGenerator mapGenerator;

    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mapGenerator.isGenerated)
        {
            if(ArrowList.Count > 0)
            {
                for (int i = 0; i < ArrowList.Count; i++)
                {
                    if (ArrowList[i].GetComponent<Rigidbody>())
                    {
                        ArrowList[i].transform.forward = ArrowList[i].GetComponent<Rigidbody>().velocity.normalized;
                    }
                }
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 TouchPos = touch.position;
                TouchPos.z = 0.5f;
                Vector3 PutPos = Camera.allCameras[0].ScreenToWorldPoint(TouchPos);
                
               
                if(TouchPos.y > 100)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        arrowGenerated = true;
                        TouchPosList = Enumerable.Repeat<Vector3>(TouchPos, 10).ToArray();

                        GameObject Arrow = Instantiate(arrowPrefab, PutPos, arCameraObject.transform.rotation);
                        ArrowList.Add(Arrow);
                        if (ArrowList.Count == 4)
                        {
                            Destroy(ArrowList[0]);
                            ArrowList.RemoveAt(0);
                        }
                    }

                    if (ArrowList.Count > 0)
                    {
                        if (touch.phase == TouchPhase.Ended)
                        {
                            Vector3 vel_disp = TouchPos - TouchPosList[count];
                            Vector3 vel_camera = new Vector3(vel_disp.x / 2, vel_disp.y / 4, 50 + vel_disp.magnitude / 3);
                            Vector3 vel_world = arCameraObject.transform.TransformDirection(vel_camera);

                            Rigidbody rb = ArrowList.Last().AddComponent<Rigidbody>();
                            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

                            //rb.mass = 0.001f;
                            rb.AddForce(vel_world);
                            //rb.AddTorque(vel_world / 10);
                        }
                        else if (touch.phase == TouchPhase.Moved)
                        {
                            TouchPosList[count] = TouchPos;
                            count++;
                            if (count == 10) { count = 0; }
                            ArrowList.Last().transform.position = PutPos;
                            ArrowList.Last().transform.rotation = arCameraObject.transform.rotation;
                        }
                    }
                }                
            }            
        }
    }
}