using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    public static TrajectoryController main;
    public GameObject carPrefab;
    
    TrajectoryRecorder recorder;
    GameObject ghostCar;

    void Awake()
    {
        main = this;
    }
    
    void Start()
    {
        recorder = GetComponent<TrajectoryRecorder>();

        if (recorder == null)
        {
            recorder = gameObject.AddComponent<TrajectoryRecorder>();
        }
    }

    public void StartRecordPath()
    {
        recorder.StartRecording();
    }

    public void CreateGhostCar()
    {
        // Delete old ghost car
        if (ghostCar != null)
        {
            Destroy(ghostCar);
        }

        var trajectory = recorder.GetRecordedTrajectory();
        if (trajectory == null || trajectory.Length == 0)
        {
            Debug.LogError("No trajectory recorded");
            return;
        }

        // Create ghost
        ghostCar = Instantiate(carPrefab, trajectory[0].position, Quaternion.Euler(trajectory[0].rotationEuler));

        var replayer = ghostCar.AddComponent<TrajectoryReplayer>();

        // Start playback        
        ghostCar.AddComponent<TrajectoryReplayer>().StartReplay(trajectory);
    }
}