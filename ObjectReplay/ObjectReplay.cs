using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class ObjectReplay : MonoBehaviour
{
	public GameObject movingObject;
	public TextAsset csvFile;

	// Update is called once per frame
	void Update() {
		readCSV();
	}

	void readCSV()
    {
		string[] records = csvFile.text.Split('\n'); // Get number of lines = number of updates

		for(int i = 1; i < records; i++) // i = 1 as start, since the top line is just a header and doesn't contain positional information
        {
			string[] fields = records[i].Split(','); // Separate from commas (Comma Separated Values)

			movingObject.transform.Position(float.Parse(fields[1]), float.Parse(fields[2]), float.Parse(fields[3]));
			movingObject.transform.Rotate(float.Parse(fields[4]), float.Parse(fields[5]), float.Parse(fields[6]));
        }
    }
}
