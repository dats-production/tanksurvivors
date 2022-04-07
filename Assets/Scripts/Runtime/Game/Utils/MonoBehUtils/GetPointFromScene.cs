using System;
using UnityEngine;

namespace Game.Utils.MonoBehUtils
{
    public class GetPointFromScene : MonoBehaviour
    {
        [Serializable]
        private struct Point
        {
            public Transform Transform;
            public string Key;
        }
        [SerializeField] private QuadArea _quadArea;
        [SerializeField] private QuadArea _saveArea;

        [SerializeField] private Point[] points;

        public Transform GetPoint(string key)
        {
            foreach (var point in points)
                if (key == point.Key)
                    return point.Transform;
            throw new Exception($"No position on scene with key {key} was found!");
        }

        public Vector3 GetRandomPoint() => LevelUtils.CalculateRandomPosition(_quadArea.point1.position, _quadArea.point2.position);

        public Vector3 GetRandomPointWithSaveArea()
        {
            Vector3 position;
            bool isInSaveArea;

            do
            {
                position = LevelUtils.CalculateRandomPosition(_quadArea.point1.position, _quadArea.point2.position);
            } while (position.ValidatePoint(_saveArea.point1.position, _saveArea.point2.position));

            return position;
        } 

        
        
        public QuadArea GetSaveArea => _saveArea;

    }
}