using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader; 
    [SerializeField] private string playCampaignLoad = "Campaign";
    [SerializeField] private string ultimateChanllengeLoad = "UltimateChallenge"; 

    private void Start()
    {
        if (sceneFader == null)
        {
            sceneFader = FindObjectOfType<SceneFader>();
        }
    }

    public void PlayCampaign()
    {
        if (sceneFader != null)
        {
            sceneFader.FadeTo(playCampaignLoad);
        }
        else
        {
            SceneManager.LoadScene(playCampaignLoad);
        }
    }

    public void PlayChallenge()
    {
        if (sceneFader != null)
        {
            sceneFader.FadeTo(ultimateChanllengeLoad);
        }
        else
        {
            SceneManager.LoadScene(ultimateChanllengeLoad);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");

        // for mobile devices
        Application.Quit();

        // for Unity Editor (for test purposes)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
