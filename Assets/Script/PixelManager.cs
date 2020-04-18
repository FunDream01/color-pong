using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelManager : MonoBehaviour
{
    public ParticleSystem PixelColorFX;
    public Vector3 FXoffset = new Vector3(0,0,-.7f);
    public Material ColoredMat;
    public bool isColored = false;
    public void ColorThePixel()
    {
        GetComponent<MeshRenderer>().material = ColoredMat;
        isColored = true;
    }
    public void ColorThePixel_CheckComplet()
    {
        isColored = true;
        PixelColorFX = GameObject.FindGameObjectWithTag("LevelGen").GetComponent<LevelGenerator>().PixelColorFX;
        ParticleSystem FX = Instantiate(PixelColorFX, this.transform.position + FXoffset, Quaternion.identity);
        //FX.GetComponent<ParticleSystemRenderer>().material.SetColor("_TintColor", this.GetComponent<MeshRenderer>().material.color);//this.GetComponent<MeshRenderer>().material.color;
        //var col = FX.colorOverLifetime;
        //col.enabled = true;
        //Gradient grad = new Gradient();
        //grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(this.GetComponent<MeshRenderer>().material.color, 1.0f)}, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        //col.color = grad;
        GetComponent<MeshRenderer>().material = ColoredMat;

        //ParticleSystem.MainModule ma = FX.main;
        //ma.startColor = ColoredMat.color;
        LevelManager.instance.CheckComplet();
    }

}
