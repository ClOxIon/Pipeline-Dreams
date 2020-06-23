using UnityEngine;

namespace PipelineDreams {
    public class Util {

        public static readonly Quaternion TurnUp = Quaternion.Euler(-90, 0, 0);
        public static readonly Quaternion TurnDown = Quaternion.Euler(90, 0, 0);
        public static readonly Quaternion TurnRight = Quaternion.Euler(0, 90, 0);
        public static readonly Quaternion TurnLeft = Quaternion.Euler(0, -90, 0);
        public static Vector3Int FaceToUVector(int f) {
            return new Vector3Int(f >> 1 == 0 ? 1 : 0, f >> 1 == 1 ? 1 : 0, f >> 1 == 2 ? 1 : 0) * (-((f & 1) << 1) + 1);
        }
        public static int FaceFlip(int f) {
            return (f % 2 == 0) ? f + 1 : f - 1;
        }

        public static int UVectorToFace(Vector3Int v) {

            return (1 - v.x) / 2 + Mathf.Abs(v.y) * (5 - v.y) / 2 + Mathf.Abs(v.z) * (9 - v.z) / 2;
        }
        /// <summary>
        /// North = (0,0,1) Up = (0,1,0)
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static int QToFace(Quaternion q) {
            var v = Vector3Int.RoundToInt(q * Vector3.forward);
            return (1 - v.x) / 2 + Mathf.Abs(v.y) * (5 - v.y) / 2 + Mathf.Abs(v.z) * (9 - v.z) / 2;
        }

        public static int QToFoot(Quaternion q)
        {
            var v = Vector3Int.RoundToInt(q * Vector3.down);
            return (1 - v.x) / 2 + Mathf.Abs(v.y) * (5 - v.y) / 2 + Mathf.Abs(v.z) * (9 - v.z) / 2;
        }

        public static Quaternion FaceToLHQ(int f) {
            return Quaternion.LookRotation(new Vector3Int(f >> 1 == 0 ? 1 : 0, f >> 1 == 1 ? 1 : 0, f >> 1 == 2 ? 1 : 0) * (-((f & 1) << 1) + 1));
        }
        /// <summary>
        /// The quaternion looks at face while its bottom is aligned to foot.
        /// </summary>
        /// <param name="face"></param>
        /// <param name="foot"></param>
        /// <returns></returns>
        public static Quaternion FaceToLHQ(int face, int foot)
        {
            return Quaternion.LookRotation(new Vector3Int(face >> 1 == 0 ? 1 : 0, face >> 1 == 1 ? 1 : 0, face >> 1 == 2 ? 1 : 0) * (-((face & 1) << 1) + 1), -new Vector3Int(foot >> 1 == 0 ? 1 : 0, foot >> 1 == 1 ? 1 : 0, foot >> 1 == 2 ? 1 : 0) * (-((foot & 1) << 1) + 1));
        }
        public static Vector3Int QToUVector(Quaternion q) {
            return Vector3Int.RoundToInt(q * Vector3.forward);
        }
        public static bool IsBasis(Vector3Int v) {
            return v.x * v.y == 0 & v.y * v.z == 0 & v.z * v.x == 0;
        }
        public static Vector3Int Normalize(Vector3Int v) {
            v.Clamp(Vector3Int.one * -1, Vector3Int.one);
            return v;
        }
        public static Quaternion RotateToFace(int f, Quaternion q0) {
            if (QToFace(q0) == f) return Quaternion.identity;
            if (QToFace(q0 * TurnUp) == f) return TurnUp;
            if (QToFace(q0 * TurnDown) == f) return TurnDown;
            if (QToFace(q0 * TurnLeft) == f) return TurnLeft;
            if (QToFace(q0 * TurnRight) == f) return TurnRight;
            return TurnUp;
        }
        
    }
}