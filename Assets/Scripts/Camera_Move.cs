using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public GameObject cutsceneCam;
    public List<GameObject> virtCams;
    int camCt;

    void Start()
    {
        camCt = 0;
    }

    void NextCam()
    {
        if (camCt < virtCams.Count) //moves to next camera
        {
            camCt++;
            virtCams[camCt].SetActive(true);
            virtCams[camCt - 1].SetActive(false);
        }
        else //resets to first camera
        {
            camCt = 0;
            virtCams[camCt].SetActive(true);
            virtCams[virtCams.Count - 1].SetActive(false);
        }
    }

    void PreviousCam(){
        if (camCt > 0) { //dont move the camera below the first screen
            camCt--;
            virtCams[camCt].SetActive(true);
            virtCams[camCt + 1].SetActive(false);
        } else {
            //reset to first camera
            camCt = 0;
            virtCams[camCt].SetActive(true);
            virtCams[virtCams.Count - 1].SetActive(false);
        }
    }
    

    void CutsceneCamSet()
    {
        foreach (GameObject camObj in virtCams)
        {
            camObj.SetActive(false);
        }

        cutsceneCam.SetActive(true);
    }
}
