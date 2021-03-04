using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPassage : MonoBehaviour
{
    TriggerManager triggerManager;
    public bool itWorks = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.GetComponent<Agent>())
        {
            triggerManager.timeTrigger[0].SetActive(true);

            for (int i = 0; i < triggerManager.timeTrigger.Length; i++)
            {
                itWorks = true;
                //triggerManager.timeTrigger[i + 1].SetActive(true);
                //triggerManager.timeTrigger[i].SetActive(false);
            }
        }
    }
}
