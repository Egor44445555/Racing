using UnityEngine;
using System;

public class TrajectoryRecorder : MonoBehaviour
{
    public bool isRecording = false;
    TrajectoryPoint[] recordedPoints = Array.Empty<TrajectoryPoint>();
    int recordCount = 0;
    float recordingStartTime;

    void Start()
    {
        recordedPoints = new TrajectoryPoint[1000];
    }

    public void StartRecording()
    {
        recordCount = 0;
        recordingStartTime = Time.time;
        isRecording = true;
    }

    void FixedUpdate()
    {
        if (!isRecording) return;

        // Increase the buffer if necessary
        if (recordCount >= recordedPoints.Length)
        {
            Array.Resize(ref recordedPoints, recordedPoints.Length * 2);
        }

        var rb = GetComponent<Rigidbody>();
        recordedPoints[recordCount++] = new TrajectoryPoint(
            Time.time - recordingStartTime,
            transform,
            rb != null ? rb.linearVelocity : Vector3.zero
        );
    }

    public TrajectoryPoint[] GetRecordedTrajectory()
    {
        var result = new TrajectoryPoint[recordCount];
        Array.Copy(recordedPoints, result, recordCount);
        return result;
    }
}