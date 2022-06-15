using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGoogle : MonoBehaviour
{
    private string latlon;

    public StreetView streetView;
    // Start is called before the first frame update
    void Start()
    {
        latlon = streetView.latlon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonTapped()
    {
        string url = "https://www.google.com/maps/@?api=1&map_action=pano&viewpoint=" + latlon;
        Application.OpenURL(url);
    }
}
