using UnityEngine;

public abstract class WildState
{
    public abstract void StartState(WildPhotomorphController wild);
    public abstract void UpdateState(WildPhotomorphController wild);
    public abstract void ChangeState(WildPhotomorphController wild);
    public abstract void ExitState(WildPhotomorphController wild);
}
