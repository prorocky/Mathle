using UnityEngine;
using System.Collections;


public class StreakSystem : MonoBehaviour {

    bool streak; //true if the play is already on a streak
    bool streakStart; //true if today is the start of the streak
    bool wed; //true if the the current day is Wednesday
    bool wasStreak; //true if previous day was a streak
    int streakCount; //
    bool win;
    //skipping a set amount of days should break the streak

    public string streakStartWord = "Streak";
    public string[] streakBank = {
        "Big Streak", "Huge Streak", "Super Streak", "Mega Streak",
        "Insane", "Unstoppable", "Incredible", "Amazing",
    };
    public string[] startBank = {
        "Nice", "Nice Work", "WOW", "Great Job", "Great Work", "Good Job", "Good Work"
    };
    public string[] wackyWednesdayBank = {
        "Streakmania", "Streaktastic", "Streakalicious", "It's Raining Streaks", "SHEEEESH" 
    };
    public string[] wackyStartBank = {
        "Streak-Startastic", "Streak-Startmania", "Streak-Startalicious", "SHHHHHEEETART"
    };
    public string[] failedBank = {
        "Oof", "F", "Tough Luck", "Get Gud", "GG"
    };
    public string[] streakBreakBank = {
        "Unfortunate", "Better Luck Next Time", "Miami YIKES", "MEGA-Oof", "F!!!"
    };

	void Start () {
	
        //check if streak is still active, if so set streak to true
        // streak = true;
	}
}