using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class LevelGenerator : MonoBehaviour
{
    public Transform Animation;
    public Material UnColoredMat;
    public Texture2D[] maps;
    public Texture2D map;
    public MeshRenderer CubeMesh;
    public List<Color32> Colors = new List<Color32>();
    public List<Material> materials = new List<Material>();
    public LevelManager[] levelManager;
    public Shader URP_Lit;
    public int PlayerLevel;
    public ParticleSystem PixelColorFX;


    void Awake()
    {
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");
            if (PlayerLevel >= maps.Length)
            {
                map = maps[Random.Range(0,maps.Length)];
            }else{
                map = maps[PlayerLevel];
            }
        }
        else
        {
            PlayerPrefs.SetInt("PlayerLevel", 0);
            PlayerLevel=PlayerPrefs.GetInt("PlayerLevel");
            map = maps[PlayerLevel];
        }
        
        GenerateMaterials();
        GenerateLevel();
    }
    void Start()
    {
        
    }

    void GenerateMaterials()
    {
        //Debug.Log("GeneratingMats");
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                FindAllColorsInMap(x, y);
            }
        }
    }
    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }
    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        Vector3 Position = new Vector3(x - (map.width / 2), y);
        // if pixel is not transparrent.
        if (pixelColor.a != 0)
        {
            foreach (Material mat in materials)
            {
                if (pixelColor.Equals(mat.GetColor("_Color")))
                {
                    MeshRenderer mesh = Instantiate(CubeMesh, Position, Quaternion.identity, transform);
                    if (!pixelColor.Equals(Color.white))
                    {
                        PixelManager pixel = mesh.gameObject.AddComponent<PixelManager>();
                        pixel.ColoredMat = mat;
                        mesh.material = UnColoredMat;
                        mesh.transform.parent=Animation;
                    }
                }
                /*else
                {
                    Debug.Log("Color Doesnt Match");
                }*/
            }
        }
    }
    Material CreateMat(Color32 PixelColor)
    {
        Material NewMaterial = new Material(Shader.Find("Standard"));
        NewMaterial.color = PixelColor;
        //NewMaterial.SetColor("_BaseColor", PixelColor);
        return NewMaterial;
    }
    void FindAllColorsInMap(int x, int y)
    {
       // Debug.Log("FindingCols");

        Color pixelColor = map.GetPixel(x, y);
        foreach (Color color in Colors)
        {
            if (color.Equals(pixelColor))
            {
                return;
            }
        }
        Colors.Add(pixelColor);
        materials.Add(CreateMat(pixelColor));
    }
}



