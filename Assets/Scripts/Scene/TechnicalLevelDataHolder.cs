using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TechnicalLevelDataHolder : MonoBehaviour
{
    [SerializeField] private List<string> _levelNames;
    [SerializeField] private List<LevelData> _levelDataList;

    public List<LevelData> LevelDataList
    {
        get { return _levelDataList; }
    }

    public LevelData GetLevelData(string name)
    {
        foreach(LevelData levelData in _levelDataList)
        {
            if (levelData.name == name)
                return levelData;
        }
        return null;
    }
}
