using System.Collections.Generic;

public class HijaiyahLetter
{
    public int id;
    public string name; 
    public string character; 

    public HijaiyahLetter(int id, string name, string character)
    {
        this.id = id;
        this.name = name;
        this.character = character;
    }
}


public static class HijaiyahDatabase
{

    public static List<HijaiyahLetter> letters = new List<HijaiyahLetter>
    {
        new HijaiyahLetter(1, "alif", "ا"),
        new HijaiyahLetter(2, "ba", "ب"),
        new HijaiyahLetter(3, "ta", "ت"),
        new HijaiyahLetter(4, "tsa", "ث"),
        new HijaiyahLetter(5, "jim", "ج"),
        new HijaiyahLetter(6, "ha", "ح"),
        new HijaiyahLetter(7, "kha", "خ"),
        new HijaiyahLetter(8, "dal", "د"),
        new HijaiyahLetter(9, "dzal", "ذ"),
        new HijaiyahLetter(10, "ra", "ر"),
        new HijaiyahLetter(11, "zai", "ز"),
        new HijaiyahLetter(12, "sin", "س"),
        new HijaiyahLetter(13, "syin", "ش"),
        new HijaiyahLetter(14, "shad", "ص"),
        new HijaiyahLetter(15, "dhad", "ض"),
        new HijaiyahLetter(16, "tha", "ط"),
        new HijaiyahLetter(17, "zha", "ظ"),
        new HijaiyahLetter(18, "ain", "ع"),
        new HijaiyahLetter(19, "ghain", "غ"),
        new HijaiyahLetter(20, "fa", "ف"),
        new HijaiyahLetter(21, "qaf", "ق"),
        new HijaiyahLetter(22, "kaf", "ك"),
        new HijaiyahLetter(23, "lam", "ل"),
        new HijaiyahLetter(24, "mim", "م"),
        new HijaiyahLetter(25, "nun", "ن"),
        new HijaiyahLetter(26, "wawu", "و"),
        new HijaiyahLetter(27, "ha_marbuthah", "ة"), // atau "ه" jika maksudnya ha biasa
        new HijaiyahLetter(28, "ya", "ي")
    };

    // label_map = {
    // 1: "alif", 2: "ba", 3: "ta", 4: "tsa", 5: "jim",
    // 6: "ha", 7: "kha", 8: "dal", 9: "dzal", 10: "ra",
    // 11: "zai", 12: "sin", 13: "syin", 14: "shad", 15: "dhad",
    // 16: "tha", 17: "zha", 18: "ain", 19: "ghain", 20: "fa",
    // 21: "qaf", 22: "kaf", 23: "lam", 24: "mim", 25: "nun",
    // 26: "wawu", 27: "ha_marbuthah", 28: "ya"
    // }

    public static HijaiyahLetter GetLetterByName(string name)
    {
        return letters.Find(l => l.name == name);
    }

    public static HijaiyahLetter GetLetterById(int id)
    {
        return letters.Find(l => l.id == id);
    }

}

