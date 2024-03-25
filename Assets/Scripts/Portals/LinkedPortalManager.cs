/* Assignment: Portal
/  Programmer: Wyatt Murray
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 3/17/2024
*/
using System.Collections.Generic;
using UnityEngine;

public class LinkedPortalManager : MonoBehaviour
{
    [SerializeField] List<LinkedPortal> mLinkedPortalList = new List<LinkedPortal>();

    /// <summary>
    /// calls the next in line portals' teleport method, with the gameobject from the prior portal
    /// </summary>
    /// <param name="GO">the GameObject to be teleported</param>
    /// <param name="portal">The portal that encountered the object</param>
    public void MoveObjectToNextPortalInList(GameObject GO, LinkedPortal portal)
    {
        //get current item location in list, then select the next
        int portalIndex = mLinkedPortalList.IndexOf(portal);
        Debug.Log(portalIndex);
        //3
        if (portalIndex == mLinkedPortalList.Count - 1)
        {
            portalIndex = 0;
        }
        else
        {
            portalIndex++;
        }
        //call teleport on that portal
        Debug.Log("portal index " + portalIndex);
        mLinkedPortalList[portalIndex].Teleport(GO);
    }

    private void OnValidate()
    {
        //get children, and grab all available portals from top to bottom and add them to the linkedportal list
    }
}
