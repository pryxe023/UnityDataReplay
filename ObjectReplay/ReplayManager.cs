using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{

    [Header("General replay settings")] // Input in the Unity inspector
    [Tooltip("Set a delay for when the replay starts playing (in seconds). This is also the delay between replay-repetitions.")]
    [SerializeField] public float waitReplay = 0.0f;
    [Tooltip("Set a scaling-factor for the positions. Recommended to keep at 1.")]
    [SerializeField] public float scalingFactor = 1.0f;
    [Tooltip("Set the replay speed. Recommended to keep at 1.")]
    [SerializeField] public float playSpeed = 1.0f;
    [Tooltip("Set the number of repetitions for the replay to run.")]
    [SerializeField] public int replayReps = 1;

    [Header("Replay controls")]
    [Tooltip("Start or stop the replay.")]
    [SerializeField] public bool startReplay = true;
    [Tooltip("Show the replay timestamp and current runtime. Mostly useful for testing and debugging.")]
    [SerializeField] public bool showTimeStamps = false;

    [Header("Objects to replay")] // Array of GameObjects that can be replayed
    [SerializeField] public GameObjectMotionReplay[] replayObjects;

    private int currentStep = 0;
    private int currentRep = 1;
    private float addedTime = 0.0f;
    private float pauseTime = 0.0f;
    private float playTime = 0.0f;

    private struct Step
    {
        public Vector3 destination;
        public Quaternion rotationgoal;
        public float timestamp;
        public float scalingRecording;
    }

    private Dictionary<int, List<Step>> allSteps = new Dictionary<int, List<Step>>();

    void Start()
    {
        addedTime = 0.0f; // Make sure this value gets cleared each time the program runs.

        TextAsset[] csvFiles = new TextAsset[replayObjects.Length];

        for (int i = 0; i < replayObjects.Length; i++)
        {
            csvFiles[i] = replayObjects[i].csvFile;

            var steps = readCSV(csvFiles[i]);
            allSteps.Add(i,steps);

            // Set the GameObject to the correct starting position and rotation
            replayObjects[i].transform.position = allSteps[i][0].destination * (scalingFactor / allSteps[i][0].scalingRecording);
            replayObjects[i].transform.rotation = allSteps[i][0].rotationgoal;
        }
    }

    void Update()
    {
        if (!startReplay)
        {
            pauseTime += Time.deltaTime;
        }

        if (startReplay)
        {
            playTime = allSteps[0][currentStep].timestamp;

            if (showTimeStamps)
            {
                // Display replay timestamp & running timestamp for debugging
                Debug.Log("Replay time: " + playTime + " | Running time: " + Time.time + " | Total paused time: " + pauseTime); 
            }

            if (currentStep < allSteps[0].Count - 1 && ((playTime / playSpeed)) + addedTime < Time.time - waitReplay - pauseTime)
            {
                ++currentStep;

                for (int i = 0; i < replayObjects.Length; i++)
                {
                    // Apply the movements and rotations
                    replayObjects[i].transform.SetPositionAndRotation(allSteps[i][currentStep].destination * (scalingFactor / allSteps[i][currentStep].scalingRecording), allSteps[i][currentStep].rotationgoal);
                }
            }
            if (currentStep == allSteps[0].Count - 1 && currentRep < replayReps)
            {
                ++currentRep;
                addedTime = Time.time;
                currentStep = 0;
            }
        }
    }

    List<Step> readCSV(TextAsset csv_fileName)
    {
        var steps = new List<Step>();
        float lastStamp = 0.0f;
        
        string[] records = csv_fileName.text.Split('\n');

        for (int i = 1; i < records.Length - 1; i++) // Start from i = 1, we don't want the headers to be included
        {
            string[] array = records[i].Split(',');

            if (lastStamp != float.Parse(array[6])) // Check for double entries (at same timestep)
            {
                steps.Add(
                new Step()
                    {
                        destination = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2])),
                        rotationgoal = Quaternion.Euler(new Vector3(float.Parse(array[3]), float.Parse(array[4]), float.Parse(array[5]))),
                        timestamp = float.Parse(array[6]),
                        scalingRecording = float.Parse(array[7])
                    }
                );

                lastStamp = float.Parse(array[6]); // Update the lastStamp variable to check in next iteration
            }
        }

        return steps;
    }
    
}