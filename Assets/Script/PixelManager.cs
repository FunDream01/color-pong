using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelManager : MonoBehaviour
{
    public Material ColoredMat;
    public void ColorThePixel(){
        GetComponent<MeshRenderer>().material=ColoredMat;
    }

}
