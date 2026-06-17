using UnityEngine;

public interface ICounterable
{

    public bool CanBeCounterable { get; }

    public void HandleCounter();
}
