using UnityEngine;

[CreateAssetMenu(menuName = ("DNAData"))]

public class DNA : ScriptableObject
{
    // Script inutile puisque nous ne stockons que le visuel et le nom sans exploiter le fait d'avoir un type d'ADN, ils ne sont pas uniques.
    public GameObject _visual;
    public string dnaName;
}
