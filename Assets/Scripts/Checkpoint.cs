using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] public Material unvisitedMaterial;
    [SerializeField] public Material visitedMaterial;
    public bool visited = false;
    public bool isStart = false;

    public void Awake() {
        ScreenLog.Log("created checkpoint");
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.materials[3] = unvisitedMaterial;

        ScreenLog.Log(renderer.materials.ToString());
    }

    public void OnCollisionEnter(Collision collision) {
        GameObject raceTrackObject = GameObject.Find("RaceTrack");
        RaceTrack raceTrack = raceTrackObject.GetComponent<RaceTrack>();
        if (collision.gameObject.tag == "car") {
            GetComponent<Renderer>().materials[3] = visitedMaterial;
            
            visited = true;

            raceTrack.completeCheckpoint(gameObject);
        }
    }
}
