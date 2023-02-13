// using UnityEngine;
// using Unity.Mathematics;

// public class SmoothBodyAnimation : MonoBehaviour
// {
//     Vector3 xp;
//     Vector3 y;
//     Vector3 yd;

//     float _w, _z, _d;
//     float k1, k2, k3;

//     [SerializeField] Transform target;

//     private void Start()
//     {
//         // SmoothBodyAnimation(f, z, r, x0); Le mettre dans un autre script mettre dans le fixedupdate transform.position = update
//     }

//     public SmoothBodyAnimation(float f, float z, float r, Vector3 x0)
//     {
//         _w = 2 * Mathf.PI * f;
//         _z = z;
//         _d = _w * Mathf.Sqrt(Mathf.Abs(z * z - 1));
//         k1 = z / (Mathf.PI * f);
//         k2 = 1 / (_w * _w);
//         k3 = r * z / _w;

//         xp = x0;
//         y = x0;
//         yd = Vector3.zero;
//     }

//     public Vector3 Update(float t, Vector3 x)
//     {
//         Vector3 xd = Vector3.zero;

//         xd = (x - xp) / t;
//         xp = x;

//         return Update(t, x, xd);
//     }

//     public Vector3 Update(float t, Vector3 x, Vector3 xd)
//     {
//         float k1Stable, k2Stable;

//         if (_w * t < _z)
//         {
//             k1Stable = k1;
//             k2Stable = Mathf.Max(k2, t * t / 2 + t * k1 / 2, t * k1);
//             k2Stable = Mathf.Max(k2Stable, t * k1);
//         }
//         else
//         {
//             float t1 = Mathf.Exp(-_z * _w * t);
//             float alpha = 2 * t1 * (_z <= 1 ? Mathf.Cos(t * _d) : math.cosh(t * _d));
//             float beta = t1 * t1;
//             float t2 = t / (1 + beta - alpha);
//             k1Stable = (1 - beta) * t2;
//             k2Stable = t * t2;
//         }

//         y = y + t * yd;
//         yd = yd + t * (x + k3 * xd - y - k1 * yd) / k2Stable;
//         return y;
//     }
// }
