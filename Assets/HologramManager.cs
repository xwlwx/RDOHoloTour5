using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;

public class HologramManager : Singleton<HologramManager> {

    public GameObject CubePrefab;

    public GameObject SpherePrefab;

    public static List<GameObject> ActiveHolograms = new List<GameObject>();

    public int ActiveHologramSize {
        get
        {
            return ActiveHolograms.Count;
        }

    }

    public void Create3DCube()
    {
        var positionCenter = Vector3.right;
        var rotation = new Quaternion(0 , 0, 0, 0);
        GameObject newObject = Instantiate(CubePrefab, positionCenter, rotation);

        if (newObject != null)
        {
            newObject.AddComponent<MeshCollider>();
            ActiveHolograms.Add(newObject);
        }
    }

    public void Create3DSphere()
    {
        var positionCenter = Vector3.left;
        var rotation = new Quaternion(0, 0, 0, 0);
        GameObject newObject = Instantiate(SpherePrefab, positionCenter, rotation);

        if (newObject != null)
        {
            newObject.AddComponent<MeshCollider>();
            ActiveHolograms.Add(newObject);
        }
    }
		
}
