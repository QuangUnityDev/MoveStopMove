using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmesh : MonoBehaviour
{

    public Material material;
    public Renderer mesh;
    public Material materials;
    private void Awake()
    {
       mesh.materials[0] = material;
       materials = transform.GetComponent<Material>();
        materials = material;
    }
}
