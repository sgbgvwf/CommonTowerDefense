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

public enum EntityType
{
    Enemy,
    DefenseTower
}

public enum TowerState
{
    Idle,
    Attack
}
/*
public enum AttackTimeType
{
    Immediately,
    Delay
}
*/
public enum DefenseTowerType
{
    None,
    ResourceTower,
    BulletTurret,
    Barrier,
    MagicalTurret,
    PlaceMakerTower,
    SnipeTurret,

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

public enum Levels
{
    level1,
    level2,
    level3,
    level4,
    level5,
    level6,
    level7,
    level8,


}
