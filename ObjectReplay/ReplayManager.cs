using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{

    [Header("General replay settings")] // Input in the Unity inspector
    [Tooltip("Set a delay for when the replay starts playing (in seconds).")]
    [SerializeField] public float waitReplay = 0.0f;
    [Tooltip("Set a scaling-factor for the positions. Recommended to keep at 1.")]
    [SerializeField] public float scalingFactor = 1.0f;
    [Tooltip("Set the replay speed. Recommended to keep at 1.")]
    [SerializeField] public float playSpeed = 1.0f;
    [Tooltip("Set the number of repetitions for the replay to run.")]
    [SerializeField] public int replayReps = 1;
    [Tooltip("Turn the replay on or off. Mostly useful for testing.")]
    [SerializeField] public bool startReplay = true;

    void Start()
    {

    }

    void Update()
    {

    }
}
