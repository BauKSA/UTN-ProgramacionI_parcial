using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ActionEvent
{
    public virtual void LocalExecute() { }
    public virtual void LocalStop() { }

    public abstract void Execute(InputAction.CallbackContext context);
    public virtual void Stop(InputAction.CallbackContext context) { }
}