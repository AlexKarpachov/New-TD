public interface IWaveManager
{
    void StartNextWave(); // — запускає наступну хвилю.
    void AddWave(WaveConfig config); // — додає нову хвилю.
}
