using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager instance;

    private TriggerPassage triggerPassage;
    //public GameObject[] timeTrigger;

    public Transform firstTrigger;

    private void Awake()
    {
        instance = this;

        //TriggerOrder();
    }

    //permet de tester la fonction in editeur
    [ContextMenu(itemName: "Set Triggers")]

    //automatise le linkage des checkpoints et reset sur le dernier checkpoint
    public void Init()
    {
        firstTrigger = transform.GetChild(0);

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            transform.GetChild(i).GetComponent<Checkpoint>().nextCheckpoint = transform.GetChild(i + 1);
        }

        transform.GetChild(transform.childCount - 1).GetComponent<Checkpoint>().nextCheckpoint = transform.GetChild(0);
    }

    /*public void TriggerOrder()
    {
        timeTrigger[0].SetActive(true);
        //firstTrigger = transform.GetChild(0);

        for (int i = 0; i < timeTrigger.Length; i++)
        {
            timeTrigger[i].SetActive(false);

        }

        for (int i = 0; i < length; i++)
        {

        }
    }*/
}
