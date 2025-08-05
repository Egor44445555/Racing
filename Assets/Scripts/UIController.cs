using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController main;
    [SerializeField] TextMeshProUGUI circleCount;

    void Awake()
    {
        main = this;
    }

    public void CheckCount(int count)
    {
        if (LevelController.main.GetMaxCircles() <= count)
        {
            circleCount.text = LevelController.main.GetMaxCircles().ToString() + "/" + LevelController.main.GetMaxCircles().ToString();
        }
        else
        {
            circleCount.text = count.ToString() + "/" + LevelController.main.GetMaxCircles().ToString();
        }
    }
    
    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}
