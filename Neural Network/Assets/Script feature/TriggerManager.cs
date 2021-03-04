using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager instance;

    private TriggerPassage triggerPassage;
    public GameObject[] timeTrigger;

    public Transform firstTrigger;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerOrder()
    {
        timeTrigger[0].SetActive(true);
        firstTrigger = transform.GetChild(0);

        for (int i = 0; i < timeTrigger.Length-1; i++)
        {
            timeTrigger[i + 1].SetActive(true);
            timeTrigger[i].SetActive(false);

        }

        for (int i = 0; i < length; i++)
        {

        }
    }
}
