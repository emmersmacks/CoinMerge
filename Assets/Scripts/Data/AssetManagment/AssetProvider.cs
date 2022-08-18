using UnityEngine;

namespace Infrastructure.AssetManagment
{
    public class AssetProvider : IAsset
    {
        public static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public static GameObject Instantiate(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }

        public static GameObject Instantiate(string path, Vector3 position, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity, parent);
        }
        
        public static OType GetObject<OType>(string path) where OType : Object
        {
            return Resources.Load<OType>(path);
        }
    }
    
}
