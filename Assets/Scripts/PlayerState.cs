using UnityEngine;

public abstract class PlayerState //: MonoBehaviour
{
    public abstract void EnterState(PlayerController player);
    public abstract void UpdateState(PlayerController player);
    public abstract void ExitState(PlayerController player);
    public abstract string GetStateName();
}