using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeLauncher : MonoBehaviour
{
    public BehaviourTree tree;
    // Start is called before the first frame update
    void Start()
    {
        tree = tree.Clone();
    }

    // Update is called once per frame
    void Update()
    {
        tree.MyUpdate(this);
    }
}
