using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JelleKUL.XRDataCollection
{
    /// <summary>
    /// Helper functions to de-serialize position and rotation data of a transform
    /// </summary>
    public class TransformSerialiser : MonoBehaviour
    {
        /// <summary>
        /// Serialize a transform into a readable json string containing a position(vector3) and a rotation(quaternion)
        /// </summary>
        /// <param name= "trans">The input transform that needs to be serialized</param>
        /// <returns>A json string containing position and rotation data</returns>
        public static string SerializeTransform(Transform trans)
        {
            SimpleTransform newTrans = new SimpleTransform(trans);
            string serializedTrans = JsonUtility.ToJson(newTrans);

            return serializedTrans;
        }

        /// <summary>
        /// Serialize a SimpleTransform into a readable json string containing a position(vector3) and a rotation(quaternion)
        /// </summary>
        /// <param name="trans">The input SimpleTransform that needs to be serialized</param>
        /// <returns>A json string containing position and rotation data</returns>
        public static string SerializeSimpleTransform(SimpleTransform trans)
        {
            string serializedTrans = JsonUtility.ToJson(trans);
            return serializedTrans;
        }
        /// <summary>
        /// De-serialize a json string into a SimpleTransform containing position and rotation data
        /// </summary>
        /// <param name="data">The json string to de-serialize</param>
        /// <returns>A SimpleTransform containing position and rotation</returns>
        public static SimpleTransform DeSerializeTransform(string data)
        {
            SimpleTransform newTrans = JsonUtility.FromJson<SimpleTransform>(data);
            return newTrans;
        }

        public static Vector3 RoundVector3(Vector3 input, int amount)
        {
            float rounding = Mathf.Pow(10, amount);
            return new Vector3(Mathf.Round(input.x * rounding) / rounding, Mathf.Round(input.y * rounding) / rounding, Mathf.Round(input.z * rounding) / rounding);
        }
        public static Quaternion RoundQuaternion(Quaternion input, int amount)
        {
            float rounding = Mathf.Pow(10, amount);
            return new Quaternion(Mathf.Round(input.x * rounding) / rounding, Mathf.Round(input.y * rounding) / rounding, Mathf.Round(input.z * rounding) / rounding, Mathf.Round(input.w * rounding) / rounding);
        }
    }

    /// <summary>
    /// A simple representation of a transform that stores the position and rotation in a serializable object
    /// </summary>
    [System.Serializable]
    public class SimpleTransform //todo remove the strings and use regular floats
    {
        public string id = "";

        public Vector3 pos = Vector3.zero;

        public Vector4 rot = Vector4.zero;

        public int fov = 0;


        public Vector3 position()
        {
            return pos;
        }
        public Quaternion rotation()
        {
            return new Quaternion(rot.x, rot.y, rot.z, rot.w);
        }

        public SimpleTransform(Transform trans)
        {
            pos = trans.position;

            rot = new Vector4(trans.rotation.x, trans.rotation.y, trans.rotation.z, trans.rotation.w);

            SetMetadata();
        }
        public SimpleTransform(Vector3 pos, Quaternion rot)
        {
            Debug.Log("Creating new Simpletransform");

            this.pos = pos;

            this.rot = new Vector4(rot.x, rot.y, rot.z, rot.w);

            SetMetadata();
        }
        public SimpleTransform(Matrix4x4 transformMatrix)
        {
            Vector3 pos = transformMatrix.GetColumn(3) - transformMatrix.GetColumn(2);
            Quaternion rot = Quaternion.LookRotation(-transformMatrix.GetColumn(2), transformMatrix.GetColumn(1));

            this.pos = pos;

            this.rot = new Vector4(rot.x, rot.y, rot.z, rot.w);

            SetMetadata();
        }

        void SetMetadata()
        {
            Debug.Log("Setting metadata");
            fov = Mathf.RoundToInt(Camera.main.fieldOfView);
            id = "img-" + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            Debug.Log("metadata set");
        }
    }
}


