/* Assignment: Portal
/  Programmer: Wyatt Murray
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 03/29/2024
/   EDIT: bugfix and logic edit 3/31/24
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaypointMovementSequencer : MonoBehaviour
{
    #region Public Fields

    // List of waypoints (as game objects)
    public List<GameObject> gameobjectWayPoints = new List<GameObject>();

    // Object to move along the waypoints
    public GameObject chosenObjectToMove;

    // Control fields for movement
    [SerializeField] private AnimationCurve speedCurveAlongTravel;
    [SerializeField] private bool goingForward = true;
    [SerializeField] private float speedOfTravel;

    #endregion

    #region Private Fields

    // Index of the next waypoint
    private int indexOfNextWaypoint = 0;

    // Last completed waypoint index (read-only for external calls)
    public int lastCompletedWaypoint { get; private set; }

    // Unity event for waypoint completion notification
    public UnityEvent<int> completedWaypoint = new UnityEvent<int>();

    #endregion

    #region MonoBehaviour Methods

    private void Start()
    {
        //set up the elevator for start
        lastCompletedWaypoint = 0;
        //begin movement
        StartMovementToNextWaypoint();
    }

    #endregion

    #region Movement Methods

    private void StartMovementToNextWaypoint()
    { 
        //Debug.Log("Start move to next waypoint"); NO LONGER NEEDED
        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        float timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            //get the speed of travel per frame and get the evaluation of the animation graph
            float speed = speedOfTravel * speedCurveAlongTravel.Evaluate(timeElapsed);
            float lerpValue = Mathf.Lerp(0f, 1f, timeElapsed * speed);
            //lerp based on the index
            chosenObjectToMove.transform.position = Vector3.Lerp(
                gameobjectWayPoints[lastCompletedWaypoint].transform.position,
                gameobjectWayPoints[indexOfNextWaypoint].transform.position,
                lerpValue);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        //when done with a movement, set the transform and invoke the unity event and next movement
        chosenObjectToMove.transform.position = gameobjectWayPoints[indexOfNextWaypoint].transform.position;
        completedWaypoint.Invoke(indexOfNextWaypoint);
        UpdateCompletionWaypoints();
    }

    #endregion

    #region Sequence Management Methods

    private void UpdateCompletionWaypoints()
    {
        //updaTE our last waypoint
        lastCompletedWaypoint = indexOfNextWaypoint;

        //logic for direction of movement
        if (goingForward)
        {
            indexOfNextWaypoint++;
            if (indexOfNextWaypoint >= gameobjectWayPoints.Count)
            {
                indexOfNextWaypoint = gameobjectWayPoints.Count - 2;
                goingForward = false;
            }
        }
        else
        {
            indexOfNextWaypoint--;
            if (indexOfNextWaypoint < 0)
            {
                indexOfNextWaypoint = 1;
                goingForward = true;
            }
        }
        //start next movement
        StartMovementToNextWaypoint();
    }

    #endregion
}

