using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    List<GameObject> checkpoints = new List<GameObject>();

    public int getNumCheckpoints() {
        return checkpoints.Count;
    }

    public void addCheckpoint(GameObject checkpointObject) {
        checkpoints.Add(checkpointObject);
    }
}
