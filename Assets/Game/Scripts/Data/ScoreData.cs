using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "New Score Data SO", menuName = "Data/Score/Create Score Data")]
    public class ScoreData : ScriptableObject
    {
        [System.Serializable]
        public class Data
        {
            public ScoreTypes scoreTypes;
            public int value;
        }

        public Data[] data;
    }
}
