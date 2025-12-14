using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Accomplish Data/LevelAccomplishDataSO")]

public class LevelAccomplishDataSO : ScriptableObject
{
    public List<Levels> levels;

    public Dictionary<Levels, bool> levelsAccomplishDict;








}
