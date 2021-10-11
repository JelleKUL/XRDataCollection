
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
        /// <param name="trans">The input transform that needs to be serialized</param>
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
    public class SimpleTransform
    {
        public string id = "";

        private string px = "";
        private string py = "";
        private string pz = "";
        
        private string rx = "";
        private string ry = "";
        private string rz = "";
        private string rw = "";

        public int fov = 0;


        public Vector3 position()
        {
            return new Vector3(float.Parse(px), float.Parse(py), float.Parse(pz));
        }
        public Quaternion rotation()
        {
            return new Quaternion(float.Parse(rx), float.Parse(ry), float.Parse(rz), float.Parse(rw));
        }

        public SimpleTransform(Transform trans, int precision = 4)
        {
            px = RoundedString(trans.position.x, precision);
            py = RoundedString(trans.position.y, precision);
            pz = RoundedString(trans.position.z, precision);

            rx = RoundedString(trans.rotation.x, precision);
            ry = RoundedString(trans.rotation.y, precision);
            rz = RoundedString(trans.rotation.z, precision);
            rw = RoundedString(trans.rotation.w, precision);

            SetMetadata();
        }
        public SimpleTransform(Vector3 pos, Quaternion rot, int precision = 4)
        {
            px = RoundedString(pos.x, precision);
            py = RoundedString(pos.y, precision);
            pz = RoundedString(pos.z, precision);

            ry = RoundedString(rot.y, precision);
            rz = RoundedString(rot.z, precision);
            rw = RoundedString(rot.w, precision);
            rx = RoundedString(rot.x, precision);

            SetMetadata();
        }
        public SimpleTransform(Matrix4x4 transformMatrix, int precision = 4)
        {
            Vector3 pos = transformMatrix.GetColumn(3) - transformMatrix.GetColumn(2);
            Quaternion rot = Quaternion.LookRotation(-transformMatrix.GetColumn(2), transformMatrix.GetColumn(1));

            px = RoundedString(pos.x, precision);
            py = RoundedString(pos.y, precision);
            pz = RoundedString(pos.z, precision);

            ry = RoundedString(rot.y, precision);
            rz = RoundedString(rot.z, precision);
            rw = RoundedString(rot.w, precision);
            rx = RoundedString(rot.x, precision);

            SetMetadata();
        }

        void SetMetadata()
        {
            fov = Mathf.RoundToInt(Camera.main.fieldOfView);
            id = "img-" + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
        }

        string RoundedString(float input, int precision)
        {
            float rounding = Mathf.Pow(10, precision);
            return (Mathf.Round(input * rounding) / rounding).ToString();
        }
    }
}


