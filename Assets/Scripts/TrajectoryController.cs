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
        if (ghostCar != null)
        {
            Destroy(ghostCar);
        }

        var trajectory = recorder.GetRecordedTrajectory();

        if (trajectory == null || trajectory.Length == 0)
        {
            Debug.LogError("Cannot create ghost - no trajectory recorded");
            return;
        }

        try
        {
            ghostCar = Instantiate(
                carPrefab,
                trajectory[0].position,
                trajectory[0].Rotation
            );

            SetupGhostCar(ghostCar, trajectory);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ghost car creation failed: {e.Message}");
        }
    }
    
    void SetupGhostCar(GameObject ghost, TrajectoryPoint[] trajectory)
    {
        var replayer = ghost.AddComponent<TrajectoryReplayer>();
        replayer.InitializeReplay(trajectory);
    }
}