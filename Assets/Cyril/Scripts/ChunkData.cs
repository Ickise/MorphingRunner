using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ChunkData"))]
public class ChunkData : ScriptableObject
{
   public GameObject _visual;
   public GameObject _pointEntry;
   public GameObject _pointExit;
   public Vector2 _size = new Vector2(10f,10f);
}
