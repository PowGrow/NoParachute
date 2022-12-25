using System;

[Serializable]
public class GameData
{

    public GameData(int selectedLevelId, int unlockedLevels)
    {
        SelectedLevelId = selectedLevelId;
        UnlockedLevels = unlockedLevels;
    }
    public static int SelectedLevelId { get; set; }
    public static int UnlockedLevels { get; set; }
}
