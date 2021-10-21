using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMotionReplay : MonoBehaviour
{
    private struct Step
    {
        public Vector3 destination;
        public Quaternion rotationgoal;
        public float timestamp;
    }

    [Header("File settings")] // Input in the Unity inspector
    [Tooltip("Select the csv-file containing the positions and rotations.")]
    [SerializeField] public TextAsset csvFile;
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
    
    private List<Step> steps;
    private int currentStep = 0;
    private int currentRep = 1;
    private float addedTime = 0.0f;
    private bool allowReplay = false;

    void Start()
    {
        readCSV(); // Obtain the data from the csv-file

        addedTime = 0.0f; // Make sure this value gets cleared each time the program runs.

        // Make sure the replay cannot be stopped/started during runtime. This would mess up replay speed.
        if (startReplay)
        {
            allowReplay = true;
        }

        // Set the GameObject to the correct starting position and rotation
        this.transform.position = steps[0].destination;
        this.transform.rotation = steps[0].rotationgoal;
    }

    void Update()
    {
        // Print the timestamp for debugging
        Debug.Log("Replay time: " + steps[currentStep].timestamp + " | Real time: " + Time.time);

        if (allowReplay)
        {
            if (currentStep < steps.Count - 1 && ((steps[currentStep].timestamp / playSpeed)) + addedTime < Time.time - waitReplay)
            {
                ++currentStep;

                // Apply the movements and rotations
                this.transform.SetPositionAndRotation(steps[currentStep].destination, steps[currentStep].rotationgoal);
            }

            if (currentStep == steps.Count - 1 && currentRep < replayReps)
            {
                ++currentRep;
                addedTime = Time.time;
                currentStep = 0;
            }
        }
    }

    void readCSV()
    {
        steps = new List<Step>();
        string[] records = csvFile.text.Split('\n');

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
    }
}
