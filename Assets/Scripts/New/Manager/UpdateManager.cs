using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    static HashSet<IUpdate> IUpdates = new HashSet<IUpdate>();
    static HashSet<ILateUpdate> ILateUpdates = new HashSet<ILateUpdate>();
    static HashSet<IFixedUpdate> IFixedUpdates = new HashSet<IFixedUpdate>();

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