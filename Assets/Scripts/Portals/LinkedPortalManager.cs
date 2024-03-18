using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedPortalManager : MonoBehaviour
{
    [SerializeField] List<LinkedPortal> mLinkedPortalList = new List<LinkedPortal>();

    public void MoveObjectToNextPortalInList(GameObject GO, LinkedPortal portal)
    {
        //get current item location in list, then select the next
        int portalIndex = mLinkedPortalList.IndexOf(portal);
        if (portalIndex + 1 > mLinkedPortalList.Count)
        {
            portalIndex = 0;
        }
        else 
        {
            portalIndex++;
        }
        //call teleport on that portal
        mLinkedPortalList[portalIndex].Teleport(GO);
    }
}
