using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    [SerializeField] public int numLaps = 1;
    List<Checkpoint> checkpoints = new List<Checkpoint>();
    int lapsCompleted = 0;

    public int getNumCheckpoints() {
        return checkpoints.Count;
    }

    public void addCheckpoint(GameObject checkpointObject) {
        checkpoints.Add(checkpointObject.GetComponent<Checkpoint>());
    }

    public void completeCheckpoint(GameObject checkpointObject) {
        Checkpoint checkpoint = checkpoints.Find(x => x == checkpointObject.GetComponent<Checkpoint>());
        
        checkpoint.visited = true;
        
        bool lapComplete = true;
        
        foreach (Checkpoint item in checkpoints) {
            if (item.visited == false) {
                lapComplete = false;
            }
        }

        if (lapComplete) {
            finishLap();
        }

    }

    public void finishLap() {
        lapsCompleted++;
    
        foreach (Checkpoint checkpoint in checkpoints) {
            checkpoint.visited = false;
        }

        if (lapsCompleted == numLaps) {
            lapsCompleted = 0;
            ScreenLog.Log("race finished!");
        }
        else {
            ScreenLog.Log("Starting lap " + lapsCompleted + 1 + " of " + numLaps);
        }
    }
}
