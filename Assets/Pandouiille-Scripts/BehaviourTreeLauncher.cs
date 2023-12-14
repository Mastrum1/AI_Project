using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeLauncher : MonoBehaviour
{
    BehaviourTree tree;
    // Start is called before the first frame update
    void Start()
    {
        tree = ScriptableObject.CreateInstance<BehaviourTree>();

        var log = ScriptableObject.CreateInstance<DebugLogNode>();
        log.message = "Hello World";

        var pause = ScriptableObject.CreateInstance<WaitNode>();

        var log2 = ScriptableObject.CreateInstance<DebugLogNode>();
        log2.message = "Hey";

        var pause2 = ScriptableObject.CreateInstance<WaitNode>();

        var log3 = ScriptableObject.CreateInstance<DebugLogNode>();
        log3.message = "YO";

        var pause3 = ScriptableObject.CreateInstance<WaitNode>();

        var seq = ScriptableObject.CreateInstance<SequencerNode>();
        seq.children.Add(log);
        seq.children.Add(pause);
        seq.children.Add(log2);
        seq.children.Add(pause2);
        seq.children.Add(log3);
        seq.children.Add(pause3);


        var repeat = ScriptableObject.CreateInstance<RepeatNode>();
        repeat.child = seq;
        
        tree.rootNode = repeat;
    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
}
