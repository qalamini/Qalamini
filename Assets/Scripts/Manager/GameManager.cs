using Unity.VisualScripting;

public static class GameManager
{
    public static HijaiyahLetter currentHarf = HijaiyahDatabase.GetLetterById(1); // Default to the first letter
    public static int score = 0;
    public static int life = 3;
    public static float countdownTime = 60;
    public static string filePath;
    public static bool isCorrect;
    public static bool isWinning = false;
    public static bool isLose = false;
    public static int harfLimit = 10;
    public static int currentHarfCount = 0;

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
