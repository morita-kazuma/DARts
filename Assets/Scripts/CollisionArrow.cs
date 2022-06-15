using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(this.GetComponent<Rigidbody>());
        Destroy(this.GetComponent<CapsuleCollider>());

        /*
        Vector3 hitPos;
        foreach (ContactPoint point in other.contacts)
        {
            hitPos = point.point;
            Debug.Log(hitPos);
        }
        */
    }
}
