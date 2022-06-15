using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StreetView : MonoBehaviour
{
    
    private GameObject[] planes = new GameObject[6];
    private float alpha = 0;

    public GameObject Plane;

    [HideInInspector] public string latlon;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(latlon);
        planes[0] = Instantiate(Plane, new Vector3(0, 0, 5), Quaternion.Euler(90, 180, 0)); //forward
        planes[1] = Instantiate(Plane, new Vector3(0, 5, 0), Quaternion.Euler(180, 180, 0)); //up
        planes[2] = Instantiate(Plane, new Vector3(0, -5, 0), Quaternion.Euler(0, 180, 0)); //down
        planes[3] = Instantiate(Plane, new Vector3(0, 0, -5), Quaternion.Euler(90, 0, 0)); //back
        planes[4] = Instantiate(Plane, new Vector3(5, 0, 0), Quaternion.Euler(90, 180, -90)); //right
        planes[5] = Instantiate(Plane, new Vector3(-5, 0, 0), Quaternion.Euler(90, 180, 90)); //left
        StartCoroutine(Method());
    }

    // Update is called once per frame
    void Update()
    {
        if(planes[5].GetComponent<Renderer>().material.mainTexture != null)
        {
            if (alpha < 1)
            {
                if (alpha == 0)
                {
                    alpha = 0.001f;
                }
                else
                {
                    alpha *= 1.2f;
                    if (alpha > 1)
                    {
                        alpha = 1;
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        planes[i].GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha);
                    }
                }
            }
        }        
    }

    private IEnumerator Method()
    {
        string[] urls = new string[6];
        urls[0] = "https://maps.googleapis.com/maps/api/streetview?size=1000x1000&location=" + latlon + "&source=outdoor&fov=90&heading=0&pitch=0&key=AIzaSyAi7mIOJmMrwO-phbfWl7dx9EpZhamewmQ";
        urls[1] = "https://maps.googleapis.com/maps/api/streetview?size=1000x1000&location=" + latlon + "&source=outdoor&fov=90&heading=0&pitch=90&key=AIzaSyAi7mIOJmMrwO-phbfWl7dx9EpZhamewmQ";
        urls[2] = "https://maps.googleapis.com/maps/api/streetview?size=1000x1000&location=" + latlon + "&source=outdoor&fov=90&heading=0&pitch=-90&key=AIzaSyAi7mIOJmMrwO-phbfWl7dx9EpZhamewmQ";
        urls[3] = "https://maps.googleapis.com/maps/api/streetview?size=1000x1000&location=" + latlon + "&source=outdoor&fov=90&heading=180&pitch=0&key=AIzaSyAi7mIOJmMrwO-phbfWl7dx9EpZhamewmQ";
        urls[4] = "https://maps.googleapis.com/maps/api/streetview?size=1000x1000&location=" + latlon + "&source=outdoor&fov=90&heading=90&pitch=0&key=AIzaSyAi7mIOJmMrwO-phbfWl7dx9EpZhamewmQ";
        urls[5] = "https://maps.googleapis.com/maps/api/streetview?size=1000x1000&location=" + latlon + "&source=outdoor&fov=90&heading=270&pitch=0&key=AIzaSyAi7mIOJmMrwO-phbfWl7dx9EpZhamewmQ";

        //1.UnityWebRequestを生成
        UnityWebRequest[] requests = new UnityWebRequest[6];
        Texture[] myTextures = new Texture[6];
        //Debug.Log(url);
        //2.SendWebRequestを実行し、送受信開始

        for (int i = 0; i < 6; i++)
        {
            requests[i] = UnityWebRequestTexture.GetTexture(urls[i]);
            yield return requests[i].SendWebRequest();

            //Debug.Log(url);
            //2.SendWebRequestを実行し、送受信開始

            //3.isNetworkErrorとisHttpErrorでエラー判定
            if (requests[i].isHttpError || requests[i].isNetworkError)
            {
                //4.エラー確認
                Debug.Log(requests[i].error);
            }
            else
            {
                myTextures[i] = DownloadHandlerTexture.GetContent(requests[i]);

                planes[i].GetComponent<Renderer>().material.mainTexture = myTextures[i];
            }
        }
    }
}
