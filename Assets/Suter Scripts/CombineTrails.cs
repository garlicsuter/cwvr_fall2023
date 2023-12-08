using UnityEngine;
using UnityEngine.UI;

public class CombineTrails : MonoBehaviour
{
    public string trailTag = "Trail";
    public GameObject combinedTrailPrefab;

    private void Start()
    {
        // Attach the method to the UI button click event
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(CombineTrailsOnClick);
        }
        else
        {
            Debug.LogError("CombineTrails script should be attached to a GameObject with a Button component.");
        }
    }

    private void CombineTrailsOnClick()
    {
        // Find all game objects with the specified tag
        GameObject[] trailObjects = GameObject.FindGameObjectsWithTag(trailTag);

        // Combine the trail objects into one
        CombineMeshes(trailObjects);
    }

    private void CombineMeshes(GameObject[] trailObjects)
    {
        // Create an empty game object to hold the combined mesh
        GameObject combinedObject = new GameObject("CombinedTrail");

        // Create a CombineInstance array to store the meshes
        CombineInstance[] combineInstances = new CombineInstance[trailObjects.Length];

        // Loop through each trail object and set it in the combineInstances array
        for (int i = 0; i < trailObjects.Length; i++)
        {
            combineInstances[i].mesh = trailObjects[i].GetComponent<MeshFilter>().sharedMesh;
            combineInstances[i].transform = trailObjects[i].transform.localToWorldMatrix;

            // Optionally, you may want to destroy the original trail objects
            //Destroy(trailObjects[i]);
        }

        // Create a new MeshFilter and MeshRenderer for the combined object
        MeshFilter meshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();

        // Combine the meshes
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combineInstances, true, true);

        // Optionally, you may want to set the material of the combined object
        meshRenderer.material = trailObjects[0].GetComponent<MeshRenderer>().sharedMaterial;

        // Optionally, you may want to add other components to the combined object, such as colliders
        // combinedObject.AddComponent<BoxCollider>();

        // Instantiate the combined object as a prefab
        Instantiate(combinedObject, combinedObject.transform.position, combinedObject.transform.rotation);

        // Optionally, you may want to destroy the empty game object
        //Destroy(combinedObject);
    }
}
