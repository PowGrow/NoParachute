using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{

    public GameData(int selectedLevelId,int lastSelectedLevelId, int unlockedLevels)
    {
        SelectedLevelId = selectedLevelId;
        LastSelectedLevelId = lastSelectedLevelId;
        UnlockedLevels = unlockedLevels;
        LevelStats = new List<LevelStats>();
        LevelStats.Add(new LevelStats(0,0,0,0));
    }
    public static int SelectedLevelId { get; set; }
    public static int LastSelectedLevelId { get; set; }
    public static int UnlockedLevels { get; set; }
    public static List<LevelStats> LevelStats { get; set; }
}
