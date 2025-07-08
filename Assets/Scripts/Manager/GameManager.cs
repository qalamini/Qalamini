public static class GameManager
{
    public static HijaiyahLetter currentHarf = HijaiyahDatabase.GetLetterById(1); // Default to the first letter
    public static int score = 0;
    public static int life = 3;
    public static float countdownTime = 60;
    public static string filePath;
    public static bool isCorrect;

    public static void ResetGame()
    {
        score = 0;
        life = 3;
        countdownTime = 60;
        filePath = string.Empty;
        isCorrect = false;
    }
}
