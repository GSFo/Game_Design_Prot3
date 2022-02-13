using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 100;
        Debug.Log("Application running at FPS: " + Application.targetFrameRate.ToString());

    }
}
