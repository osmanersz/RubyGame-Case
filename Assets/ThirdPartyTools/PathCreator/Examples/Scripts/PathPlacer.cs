using PathCreation;
using UnityEditor;
using UnityEngine;

namespace PathCreation.Examples {

    [ExecuteInEditMode]
    public class PathPlacer : PathSceneTool
    {
        public bool otoDestroy;
        public GameObject prefab;
        public GameObject holder;
        public float spacing = 3;
        [Range(1,50)]
        public float maxObjectCount = 2;
        [Range(-5f,5)]
        public float xOffset = 1;
        [Range(0.1f,5)]
        public float yOffset = 1;
        [Range(-5f,5)]
        public float zOffset = 1;

        const float minSpacing = .1f;

        void Generate()
        {
            if (pathCreator != null && prefab != null && holder != null)
            {
                if (!otoDestroy)
                    return;
                
                    DestroyObjects();

                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(minSpacing, spacing);
                float dst = 0;
                int objCounter = 0;
                while (dst < path.length)
                {
                    if (objCounter < maxObjectCount)
                    {
                        Vector3 point = path.GetPointAtDistance(dst);
                        point -= new Vector3(xOffset, yOffset, zOffset);
                        Quaternion rot = path.GetRotationAtDistance(dst);
                        //  Instantiate (prefab, point, rot, holder.transform);
#if UNITY_EDITOR

                        Object obj = PrefabUtility.InstantiatePrefab(prefab);
                        if (!(obj is GameObject holderObj)) return;
                        holderObj.transform.position = point;
                        holderObj.transform.rotation = rot;
                        holderObj.transform.parent = holder.transform;
#endif
                        dst += spacing;
                        objCounter++;
                    }
                    else
                        break;
                }
            }
        }

        void DestroyObjects () {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--) {
                DestroyImmediate (holder.transform.GetChild (i).gameObject, false);
            }
        }

        protected override void PathUpdated () {
            if (pathCreator != null) {
                Generate ();
            }
        }
    }
}