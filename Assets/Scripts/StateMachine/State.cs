using UnityEngine;

public abstract class State
{
    public readonly string name;

    protected State(string name)
    {
        this.name = name;
    }
    public virtual void Enter()
    {
        // Code to execute when entering the state
    }
    public virtual void Exit()
    {
        // Code to execute when exiting the state
    }
    public virtual void Update()
    {
        // Code to execute every frame while in the state
    }
    public virtual void LateUpdate()
    {
        // Code to execute every frame after Update while in the state
    }
    public virtual void FixedUpdate()
    {
        // Code to execute every fixed frame while in the state
    }
}