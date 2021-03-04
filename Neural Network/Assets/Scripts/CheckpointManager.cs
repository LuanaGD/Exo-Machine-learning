using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    public Transform firstCheckpoint;

    private void Awake()
    {
        instance = this;
    }

    //permet de tester la fonction in editeur
    [ContextMenu(itemName:"Set Checkpoints")]

    //automatise le linkage des checkpoints et reset sur le dernier checkpoint
    public void Init()
    {
        firstCheckpoint = transform.GetChild(0);

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            transform.GetChild(i).GetComponent<Checkpoint>().nextCheckpoint = transform.GetChild(i + 1);
        }

        transform.GetChild(transform.childCount - 1).GetComponent<Checkpoint>().nextCheckpoint = transform.GetChild(0);
    }
}
