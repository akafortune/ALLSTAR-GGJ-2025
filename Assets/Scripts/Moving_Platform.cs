using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    public Transform pt1;
    public Transform pt2;

    Vector3 nextPt;

    void Start()
    {
        nextPt = pt1.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, nextPt) < 0.2f)
        {
            if (nextPt == pt1.position)
            {
                nextPt = pt2.position;
            }
            else
            {
                nextPt = pt1.position;
            }
        }
 
        transform.position = Vector3.MoveTowards(transform.position, nextPt, Time.deltaTime * 7f);
    }

}
