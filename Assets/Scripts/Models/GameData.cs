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
        LevelStats.Add(new LevelStats(0,0,0,0,new bool[3] { false, false, false }));
    }
    public int SelectedLevelId { get; set; } = 0;
    public int LastSelectedLevelId { get; set; } = 0;
    public int UnlockedLevels { get; set; } = 0;
    public List<LevelStats> LevelStats { get; set; }
}
