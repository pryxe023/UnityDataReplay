using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTest : MonoBehaviour
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

    [Header("Objects to replay")] // Array of GameObjects that can be replayed
    [SerializeField] public ReplayTest[] replayObjects;

    private int currentStep = 0;
    private int currentRep = 1;
    private float addedTime = 0.0f;
    private bool allowReplay = false;

    private struct Step
    {
        public Vector3 destination;
        public Quaternion rotationgoal;
        public float timestamp;
    }

    private Dictionary<int, List<Step>> allSteps = new Dictionary<int, List<Step>>();

    void Start()
    {
        addedTime = 0.0f; // Make sure this value gets cleared each time the program runs.

        // Make sure the replay cannot be stopped/started during runtime. This would mess up replay speed.
        if (startReplay)
        {
            allowReplay = true;
        }

        TextAsset[] csvFiles = new TextAsset[replayObjects.Length];

        for (int i = 0; i < replayObjects.Length; i++)
        {
            csvFiles[i] = replayObjects[i].csvFile;

            var steps = readCSV(csvFiles[i]);
            allSteps.Add(i,steps);

            // Set the GameObject to the correct starting position and rotation
            replayObjects[i].transform.position = allSteps[i][0].destination;
            replayObjects[i].transform.rotation = allSteps[i][0].rotationgoal;
        }
    }

    void Update()
    {
        for (int i = 0; i < replayObjects.Length; i++)
        {
            // Print the timestamp for debugging
            Debug.Log("Replay time: " + allSteps[i][currentStep].timestamp + " | Real time: " + Time.time);

            if (allowReplay)
            {
                if (currentStep < allSteps[i].Count - 1 && ((allSteps[i][currentStep].timestamp / playSpeed)) + addedTime < Time.time - waitReplay)
                {
                    ++currentStep;

                    // Apply the movements and rotations
                    replayObjects[i].transform.SetPositionAndRotation(allSteps[i][currentStep].destination, allSteps[i][currentStep].rotationgoal);
                }

                if (currentStep == allSteps[i].Count - 1 && currentRep < replayReps)
                {
                    ++currentRep;
                    addedTime = Time.time;
                    currentStep = 0;
                }
            }
        }
    }

    List<Step> readCSV(TextAsset csv_fileName)
    {
        var steps = new List<Step>();
        
        string[] records = csv_fileName.text.Split('\n');

        for (int i = 1; i < records.Length - 1; i++) // Start from i = 1, we don't want the headers to be included
        {
            string[] array = records[i].Split(',');

            steps.Add(
            new Step()
                {
                    destination = new Vector3(float.Parse(array[1]) * scalingFactor, float.Parse(array[2]) * scalingFactor, float.Parse(array[3]) * scalingFactor),
                    rotationgoal = new Quaternion(float.Parse(array[4]), float.Parse(array[5]), float.Parse(array[6]), float.Parse(array[7])),
                    timestamp = float.Parse(array[8])
                }
            );
        }

        return steps;
    }
    
}