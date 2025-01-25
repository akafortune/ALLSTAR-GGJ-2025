using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Checker : MonoBehaviour
{
    public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        grounded= false;
    }
}
