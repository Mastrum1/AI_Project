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

        tree.rootNode = log;

        var repeat = ScriptableObject.CreateInstance<RepeatNode>();
        repeat.child = log;
    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
}
