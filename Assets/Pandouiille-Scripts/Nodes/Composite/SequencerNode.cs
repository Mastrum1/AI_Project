public class SequencerNode : CompositeNode
{
    int current;

    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        current = 0;
    }

    protected override void OnStop(BehaviourTreeLauncher launcher   )
    {

    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        for (int i = current; i < children.Count; ++i)
        {
            current = i;
            var child = children[current];

            switch (child.MyUpdate(launcher))
            {
                case State.Running:
                    return State.Running;
                case State.Failure:
                    return State.Failure;
                case State.Success:
                    continue;
            }
        }
        return State.Success;
    }
}
