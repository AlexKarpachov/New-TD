public interface IGameManager
{
    void PauseGame();
    void ResumeGame();
    void SaveProgress();
    void LoadProgress();

    void SetNormalSpeed();  
    void SetFastSpeed();    
}
