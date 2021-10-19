using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMotionReplay : MonoBehaviour
{
    private struct Step
    {
        public float speed;
        public Vector3 destination;
        public Quaternion rotationgoal;
        public float timestamp;
    }

    [Header("Replay settings")] // Input in the Unity inspector
    [Tooltip("Select the csv-file containing the positions and rotations.")]
    [SerializeField] public TextAsset csvFile;
    [Tooltip("Set the speed to replay the recorded motion.")]
    [SerializeField] public float speed = 1.5f;
    [Tooltip("Set a scaling-factor for the positions.")]
    [SerializeField] public float scalingFactor = 1.0f;
    private List<Step> steps;
    private int currentStep = 0;

    void Start()
    {
        readCSV(); // Obtain the data from the csv-file

        // Set the GameObject to the correct starting position and rotation
        this.transform.position = steps[0].destination;
        this.transform.rotation = steps[0].rotationgoal;
    }

    void Update()
    {
        ++currentStep;

        // Apply the movements and rotations
        this.transform.position = Vector3.MoveTowards(this.transform.position, steps[currentStep].destination, steps[currentStep].speed * Time.deltaTime);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, steps[currentStep].rotationgoal, steps[currentStep].speed * Time.deltaTime);

        // Print the timestamp for debugging
        Debug.Log(steps[currentStep].timestamp);
    }

    void readCSV()
    {
        steps = new List<Step>();
        string[] records = csvFile.text.Split('\n');
        for (int i = 1; i < records.Length; i++) // Start from i = 1, we don't want the headers to be included
        {
            string[] array = records[i].Split(',');

            steps.Add(
                new Step()
                {
                    speed = speed,
                    destination = new Vector3(float.Parse(array[1]) * scalingFactor, float.Parse(array[2]) * scalingFactor, float.Parse(array[3]) * scalingFactor),
                    rotationgoal = new Quaternion(float.Parse(array[4]), float.Parse(array[5]), float.Parse(array[6]), float.Parse(array[7])),
                    timestamp = float.Parse(array[8])
                }
            );
        }
    }
}
