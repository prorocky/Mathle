using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakSave
{

    public int streak = 0;
    public int date = 0;
    public string streakText = "";
    public int gamesPlayed = 0;
    public int wins = 0;

    public StreakSave(int streak, int date, string streakText, int gamesPlayed, int wins) {
        this.streak = streak;
        this.date = date;
        this.streakText = streakText;
        this.gamesPlayed = gamesPlayed;
        this.wins = wins;
    }
}
