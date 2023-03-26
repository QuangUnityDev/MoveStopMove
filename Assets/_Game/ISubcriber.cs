public interface ISubcriber
{
    void GamePrepare();
    void GameStart();
    void GameRevival();
    void GamePause();
    void GameResume();
    void GameOver();
    void GameCompleted();
}
