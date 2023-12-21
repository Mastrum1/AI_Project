using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : DecoratorNode
{
    public bool restartOnSuccess = true;
    public bool restartOnFailure = false;
    public int maxRepeats = 0;

    int iterationCount = 0;

    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        iterationCount = 0;
    }

    protected override void OnStop(BehaviourTreeLauncher launcher)
    {
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        if (child == null)
        {
            return State.Failure;
        }

        switch (child.MyUpdate(launcher))
        {
            case State.Running:
                break;
            case State.Failure:
                if (restartOnFailure)
                {
                    iterationCount++;
                    if (iterationCount >= maxRepeats && maxRepeats > 0)
                    {
                        return State.Failure;
                    }
                    else
                    {
                        return State.Running;
                    }
                }
                else
                {
                    return State.Failure;
                }
            case State.Success:
                if (restartOnSuccess)
                {
                    iterationCount++;
                    if (iterationCount >= maxRepeats && maxRepeats > 0)
                    {
                        return State.Success;
                    }
                    else
                    {
                        return State.Running;
                    }
                }
                else
                {
                    return State.Success;
                }
        }
        return State.Running;
    }
}
