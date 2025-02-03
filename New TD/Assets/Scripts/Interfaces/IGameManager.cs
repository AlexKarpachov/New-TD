public interface IGameManager
{
    void PauseGame(); // — ставить гру на паузу.
    void ResumeGame(); // — знімає паузу.
    void SaveProgress(); // — зберігає прогрес гри.
    void LoadProgress(); // — завантажує прогрес гри.
}
