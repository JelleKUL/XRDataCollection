using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// spawn an object in the scene
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    
    /// <summary>
    /// spawn an object at this objects position
    /// </summary>
    public void SpawnObjectLocally(bool copyRotation)
    {
        Instantiate(spawnObject, transform.position, copyRotation?transform.rotation: Quaternion.identity);
    }

    /// <summary>
    /// spawn an object at the targets parent transform
    /// </summary>
    /// <param name="target"> The target transform</param>
    /// <param name="parent">Parent the spawned object to the target?</param>
    /// <returns>the spawned gameobject</returns>
    public GameObject SpawnObject(Transform target, bool parent)
    {
        GameObject newObject;
        if (parent) newObject = Instantiate(spawnObject,target);
        else newObject = Instantiate(spawnObject, transform.position, transform.rotation);

        return newObject;
    }
}
