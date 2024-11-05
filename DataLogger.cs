using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    private List<string> logData = new List<string>();
    private string filePath;
    private bool isLogging = false;

    void Start()
    {

        string logsFolderPath = Application.dataPath + "/Logs";
        if (!Directory.Exists(logsFolderPath))
        {
            Directory.CreateDirectory(logsFolderPath);
        }
        // Set the file path
        filePath = Application.dataPath + "/Logs/dataLog.csv";

        // Initialize the file with headers
        logData.Add("Time,PositionX,PositionY,PositionZ,Speed,SteeringAngle");
    }

    void FixedUpdate()
    {
        if (isLogging)
        {
            //Call log data from another script
            AIController aiController = GetComponent<AIController>();
            if(aiController != null) 
            {
                aiController.CaptureData();
            }
        }
    }

    public void StartLogging()
    {
        isLogging = true;
    }

    public void StopLogging() 
    {
        isLogging = false;
        SaveLog();
    }

    public void LogData(Vector3 position, float speed, float steeringAngle)
    {
        // Create a log entry
        string logEntry = string.Format("{0},{1},{2},{3},{4},{5}",
            Time.time, position.x, position.y, position.z, speed, steeringAngle);

        logData.Add(logEntry);
    }

    private void SaveLog()
    {
        //Write all logged data to the file
        File.WriteAllLines(filePath, logData.ToArray());
    }

    void OnApplicationQuit()
    {
        // Write all logged data to the file on application quit
        SaveLog();
    }
}

