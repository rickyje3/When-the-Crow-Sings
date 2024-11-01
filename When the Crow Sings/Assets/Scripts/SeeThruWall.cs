using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThruWall : MonoBehaviour
{
    public List<Material> SeeThruMaterials = new List<Material>(); // List of matching materials
    public Camera Camera;
    public LayerMask mask; // Assign this to wall layer

    public static int PosID = Shader.PropertyToID("_Position");
    public static int SizeID = Shader.PropertyToID("_Size");

    public string seeThruWalls = "Shader Graphs/SeeThruWalls"; // Shader name to search for

    private void Awake()
    {
        if (Camera == null)
        {
            Camera = FindObjectOfType<Camera>();
        }
    }

    private void Start()
    {
        FindMaterialsWithShader(seeThruWalls);
    }

    void Update()
    {
        var dir = Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);

        bool isInView = Physics.Raycast(ray, 3000, mask);

        foreach (var material in SeeThruMaterials)
        {
            material.SetFloat(SizeID, isInView ? 1f : 0);

            var view = Camera.WorldToViewportPoint(transform.position);
            material.SetVector(PosID, view);
        }
    }

    void FindMaterialsWithShader(string shaderName)
    {
        Renderer[] renderers = FindObjectsOfType<Renderer>(); // Get all renderers in the scene
        Debug.Log("Renderers: " + renderers.Length);
        foreach (var renderer in renderers)
        {
            foreach (var material in renderer.sharedMaterials) // Use shared materials to avoid instantiation
            {
                Debug.Log($"Checking material: {material.name} with shader: {material.shader.name}");

                if (material.shader.name == shaderName)
                {
                    if (material != null && material.shader.name == shaderName)
                    {
                        SeeThruMaterials.Add(material);
                        Debug.Log($"Added material: {material.name} to the SeeThruMaterials list");
                    }
                }
            }
        }

        Debug.Log($"Found {SeeThruMaterials.Count} materials with the shader: {seeThruWalls}");
    }
}

