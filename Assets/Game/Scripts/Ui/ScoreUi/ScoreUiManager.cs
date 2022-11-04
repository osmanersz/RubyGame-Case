using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Scripts.Data;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Ui.ScoreUi
{
    public class ScoreUiManager : MonoBehaviour
    {
        [SerializeField] private ScoreData scoreDataSO;
        private readonly Dictionary<ScoreTypes, ScoreData.Data> scoreDataDictionary = new Dictionary<ScoreTypes, ScoreData.Data>();
        [System.Serializable]
        public class ScoreElements
        {
            public ScoreTypes scoreTypes;
            public RectTransform icon;
            public TextMeshProUGUI valueTmp;
        }

        public ScoreElements[] scoreElementsArray;
        private readonly Dictionary<ScoreTypes, ScoreElements> scoreElementsDictionary = new Dictionary<ScoreTypes, ScoreElements>();
        public static event Action<ScoreTypes> CollectableCollected;

        private void OnEnable()
        {
            CollectableCollected += CollectableCollect;
        }

        private void OnDisable()
        {
            CollectableCollected -= CollectableCollect;
        }

        private void Awake()
        {
            foreach (var scoreElement in scoreElementsArray)
            {
                if (!scoreElementsDictionary.ContainsKey(scoreElement.scoreTypes))
                {
                    scoreElementsDictionary.Add(scoreElement.scoreTypes, scoreElement);
                }
            }

            if (!scoreDataSO) return;
            foreach (var data in scoreDataSO.data)
            {
                scoreDataDictionary.Add(data.scoreTypes, data);
                WriteValue(data.scoreTypes);
            }
        }
        private void CollectableCollect(ScoreTypes scoreType)
        {
            scoreDataDictionary[scoreType].value += 1;
            WriteValue(scoreType);
        }

        private void WriteValue(ScoreTypes scoreType)
        {
            if (!scoreElementsDictionary.TryGetValue(scoreType, out ScoreElements elements)) return;
            elements.valueTmp.text = scoreDataDictionary[scoreType].value.ToString();
            elements.icon.DOShakeScale(0.1f, 0.5f, 5, 1).OnComplete(FixIcons);
        }

        private void FixIcons()
        {
            foreach (var elements in scoreElementsArray)
            {
                elements.icon.localScale=Vector3.one;
            }
        }

        public static void OnCollectableCollected(ScoreTypes obj)
        {
            CollectableCollected?.Invoke(obj);
        }
    }
}
