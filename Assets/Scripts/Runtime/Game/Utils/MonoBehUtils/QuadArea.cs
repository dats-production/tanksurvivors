using UnityEngine;

namespace Game.Utils.MonoBehUtils
{
    public class QuadArea : MonoBehaviour
    {
        public Transform point1;
        public Transform point2;

        [SerializeField] private Color gizmoColor;

        private void OnDrawGizmos()
        {
            if (point1 == null || point2 == null)
                return;
            Gizmos.color = gizmoColor;
            var pointA = point1.position;
            var pointB = point2.position;
            var center = new Vector3((pointA.x + pointB.x) / 2, (pointA.y + pointB.y) / 2, (pointA.z + pointB.z) / 2);
            var size = new Vector3(
                Vector3.Distance(center, new Vector3(pointA.x, center.y, center.z)) * 2,
                1,
                Vector3.Distance(center, new Vector3(center.x, center.y, pointA.z)) * 2
            );
            Gizmos.DrawWireCube(center, size);
        }
    }
}
