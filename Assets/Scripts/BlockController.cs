using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    private bool topVis, botVis, leftVis, rightVis, frontVis, backVis;
    private bool air;

    // Start is called before the first frame update
    void Start()
    {
        air = false;
        topVis = false;
        botVis = false;
        leftVis = false;
        rightVis = false;
        frontVis = false;
        backVis = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
