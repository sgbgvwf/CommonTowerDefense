public enum MousePointState
{
    Air,//¿ÕÆø
    Place,//¿É·ÅÖÃ
    DefenseTower//·ÀÓùËş
}

public enum BuffState
{
    None,
    Burn,//È¼ÉÕ
    Cold,//º®Àä
    Slow,//»ºÂı
    InWater//Ë®ÖĞ
}

public enum AttackDetectionTarget
{
    Enemy,
    DefenseTower
}

public enum TowerState
{
    Idle,
    Attack
}

public enum AttackTimeType
{
    Immediately,
    Delay
}

public enum DefenseTowerType
{
    None,
    ResourceTower,


}

public enum EnemyState
{
    Idle,
    Move,
    Attack,


}

public enum SceneType
{
    Location, 
    Menu

}


