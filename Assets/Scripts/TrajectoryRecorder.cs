using UnityEngine;
using System;

public class TrajectoryRecorder : MonoBehaviour
{
    public bool isRecording = false;
    TrajectoryPoint[] recordedPoints = Array.Empty<TrajectoryPoint>();
    int recordCount = 0;
    float recordingStartTime;

    void Awake()
    {
        recordedPoints = new TrajectoryPoint[10000];
    }

    public void StartRecording()
    {
        recordCount = 0;
        recordingStartTime = Time.time;
        isRecording = true;

        if (recordedPoints == null || recordedPoints.Length == 0)
        {
            recordedPoints = new TrajectoryPoint[10000];
        }
    }

    void Update()
    {
        if (!isRecording) return;

        // #if UNITY_WEBGL
        //     if (Time.frameCount % 2 == 0) return;
        // #endif

        if (recordCount >= recordedPoints.Length)
        {
            Debug.LogWarning("Recording buffer full");
            isRecording = false;
            return;
        }

        try
        {
            var rb = GetComponent<Rigidbody>();
            recordedPoints[recordCount] = new TrajectoryPoint(Time.time - recordingStartTime, transform, rb != null ? rb.linearVelocity : Vector3.zero);
            recordCount++;
        }
        catch (Exception e)
        {
            Debug.LogError($"Recording failed: {e.Message}");
            isRecording = false;
        }
    }

    public TrajectoryPoint[] GetRecordedTrajectory()
    {
        if (recordedPoints == null || recordCount == 0)
        {
            Debug.LogWarning("No points recorded - returning empty array");
            return new TrajectoryPoint[0];
        }

        var result = new TrajectoryPoint[recordCount];
        
        try
        {
            for (int i = 0; i < recordCount; i++)
            {
                result[i] = recordedPoints[i];
            }
            return result;
        }
        catch (Exception e)
        {
            Debug.LogError($"Trajectory copy failed: {e.Message}");
            return new TrajectoryPoint[0];
        }
    }
}