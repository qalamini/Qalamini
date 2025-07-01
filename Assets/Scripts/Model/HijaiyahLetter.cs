using System.Collections.Generic;

public class HijaiyahLetter
{
    public string name; // nama latin (Alif, Ba, Ta, dst)
    public string character; // karakter arab jika ingin di future, misal "ا"

    public HijaiyahLetter(string name, string character)
    {
        this.name = name;
        this.character = character;
    }
}


public static class HijaiyahDatabase
{
    public static List<HijaiyahLetter> letters = new List<HijaiyahLetter>
    {
        new HijaiyahLetter("Alif", "ا"),
        new HijaiyahLetter("Ba", "ب"),
        new HijaiyahLetter("Ta", "ت"),
        new HijaiyahLetter("Tsa", "ث"),
        new HijaiyahLetter("Jim", "ج"),
        new HijaiyahLetter("Ha", "ح"),
        new HijaiyahLetter("Kha", "خ"),
        new HijaiyahLetter("Dal", "د"),
        // Tambahkan semua sampai Ya
        new HijaiyahLetter("Ya", "ي")
    };

    public static HijaiyahLetter GetLetterByName(string name)
    {
        return letters.Find(l => l.name == name);
    }
}

