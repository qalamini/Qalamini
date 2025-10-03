using Unity.VisualScripting;

public static class GameManager
{
    public static HijaiyahLetter currentHarf = HijaiyahDatabase.GetLetterById(1); // Default to the first letter
    public static int score;
    public static int life;
    public static float countdownTime;
    public static string filePath;
    public static bool isCorrect;
    public static bool isWinning;
    public static bool isLose;
    public static int harfLimit = 20;
    public static int currentHarfCount;

    public static void ResetGame()
    {
        score = 0;
        life = 3;
        countdownTime = 60;
        filePath = string.Empty;
        isCorrect = false;
        isWinning = false;
        isWinning = false;
        currentHarfCount = 0;
    }
}
