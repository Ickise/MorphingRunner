using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    // Ce script est très utile pour de l'optimisation. C'est un UpdateManager et permet de n'avoir qu'une seule Update, FixedUpdate et LateUpdate dans le jeu.
    // Si un script hérite de l'une des interfaces IUpdate, ILateUpdate ou IFixedUpdate, alors il peut s'abonner à l'UpdateManager et bénéficier des 
    // différentes fonctions du script UpdateManager.

    static HashSet<IUpdate> IUpdates = new HashSet<IUpdate>();
    static HashSet<ILateUpdate> ILateUpdates = new HashSet<ILateUpdate>();
    static HashSet<IFixedUpdate> IFixedUpdates = new HashSet<IFixedUpdate>();

    
    // Ici nous retrouvons les fonctions pour s'abonner ou se désabonner de l'Update, FixedUpdate ou LateUpdate.
    public static void RegisterUpdate(IUpdate obj)
    {
        IUpdates.Add(obj);
    }

    public static void UnregisterUpdate(IUpdate obj)
    {
        IUpdates.Remove(obj);
    }

    public static void RegisterLateUpdate(ILateUpdate obj)
    {
        ILateUpdates.Add(obj);
    }

    public static void UnregisterLateUpdate(ILateUpdate obj)
    {
        ILateUpdates.Remove(obj);
    }

    public static void RegisterFixedUpdate(IFixedUpdate obj)
    {
        IFixedUpdates.Add(obj);
    }

    public static void UnregisterFixedUpdate(IFixedUpdate obj)
    {
        IFixedUpdates.Remove(obj);
    }

    // Ici nous retrouvons les fonctions pour lancer l'Update, la FixedUpdate ou la LateUpdate.
    private void Update()
    {
        HashSet<IUpdate>.Enumerator e = IUpdates.GetEnumerator();

        while (e.MoveNext())
        {
            e.Current.UpdateTick();
        }
    }

    private void LateUpdate()
    {
        HashSet<ILateUpdate>.Enumerator e = ILateUpdates.GetEnumerator();
        while (e.MoveNext())
        {
            e.Current.LateUpdateTick();
        }
    }

    private void FixedUpdate()
    {
        HashSet<IFixedUpdate>.Enumerator e = IFixedUpdates.GetEnumerator();
        while (e.MoveNext())
        {
            e.Current.FixedUpdateTick();
        }
    }
}