using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class LevelGenerator : MonoBehaviour 
{
	public Material UnColoredMat;
	public Texture2D map;
	public MeshRenderer CubeMesh;
	public List<Color32> Colors=new List<Color32>();
	public List<Material> materials=new List<Material>();
	public LevelManager levelManager;
	void  Awake() {
		
        GenerateMaterials();
		GenerateLevel();
	}
	void Start () 
	{

	}

	void GenerateMaterials ()
	{
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
	void GenerateTile (int x, int y)
	{
		Color pixelColor = map.GetPixel(x, y);
		Vector3 Position = new Vector3(x-(map.width/2),y);
		// if pixel is not transparrent.
		if (pixelColor.a != 0)
		{
			foreach (Material mat in materials)
		 	{
				if(pixelColor.Equals(mat.color))
				{
				    MeshRenderer mesh = Instantiate(CubeMesh,Position,Quaternion.identity,transform);
					if (!pixelColor.Equals(Color.white)){
						PixelManager pixel= mesh.gameObject.AddComponent<PixelManager>();
						pixel.ColoredMat=mat;
						mesh.material=UnColoredMat;
					}
			    }
		    }
		}
	}
    Material CreateMat(Color32 PixelColor)
	{
        Material NewMaterial = new Material(Shader.Find("Standard"));
        NewMaterial.color = PixelColor;
		return NewMaterial;
    }
	void FindAllColorsInMap(int x,int y )
	{
		Color pixelColor = map.GetPixel(x, y);
		foreach (Color color in Colors)
		{
			if(color.Equals(pixelColor))
			{
				return;
			}
		}
		Colors.Add(pixelColor);
		materials.Add(CreateMat(pixelColor));
	}
}
public class Generator:Editor

{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if(GUILayout.Button("Generator Level"))
		{
		    //GenerateMaterials();
		    //GenerateLevel();
		}
	}
}

