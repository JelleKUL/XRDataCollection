using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JelleKUL.XRDataCollection
{
    /// <summary>
    /// spawn objects in the scene
    /// </summary>
    public class ObjectSpawner : MonoBehaviour
    {
        public static void SpawnImage(Texture2D targetTexture, SimpleTransform simpleTransform, Shader textureShader, float distance = 1, Transform parent = null)
        {
            //create a texture2D and load the image as a texture
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Quad);

            if (parent != null)
            {
                newObj.transform.parent = parent;
            }

            float ratio = targetTexture.width / (float)targetTexture.height;
            simpleTransform.fov = Mathf.Clamp(simpleTransform.fov, 0, 179);
            float height = 2 * Mathf.Tan(simpleTransform.fov / 2f * Mathf.Deg2Rad) * distance;
            newObj.transform.localScale = new Vector3(ratio, 1, 1) * height;

            Renderer quadRenderer = newObj.GetComponent<Renderer>();
            quadRenderer.material = new Material(textureShader);
            quadRenderer.sharedMaterial.SetTexture("_MainTex", targetTexture);

            newObj.name = simpleTransform.id;
            newObj.transform.position = simpleTransform.position();
            newObj.transform.rotation = simpleTransform.rotation();
        }

        public static void SpawnMesh(Mesh targetMesh, SimpleTransform simpleTransform, Shader meshShader, Transform parent = null)
        {
            GameObject newObj = new GameObject();

            if (parent != null)
            {
                newObj.transform.parent = parent;
            }

            newObj.AddComponent<MeshFilter>();
            newObj.AddComponent<MeshRenderer>();

            MeshFilter meshFilter = newObj.GetComponent<MeshFilter>();
            meshFilter.sharedMesh = targetMesh;
            meshFilter.sharedMesh.RecalculateBounds();

            MeshRenderer meshRenderer = newObj.GetComponent<MeshRenderer>();
            meshRenderer.material = new Material(meshShader);

            newObj.name = simpleTransform.id;
            newObj.transform.position = simpleTransform.position();
            newObj.transform.rotation = simpleTransform.rotation();
        }
    }
}
