using UnityEngine;

[CreateAssetMenu(menuName = ("ChunkData"))]
public class ChunkData : ScriptableObject
{
   // Tout simplement les données de nos chunk, peut être l'une des seules choses "optimisée" dans notre projet.
   // Encore une fois, la nomenclature n'est pas bonne avec _ devant les variables.
   public GameObject _visual;
   public GameObject _pointEntry;
   public GameObject _pointExit;
   public Vector3 _size = new Vector3(0f,0f,0f);
}
