//
//   CONTRIBUTORS: Montana Games (www.montana-games.com)                  
//   Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com    
//   -------------------------------------------------------------------------------------------------
//

using UnityEngine;

/// <summary>
/// Характеристики Врагов
/// 
/// </summary>
[CreateAssetMenu(fileName = "EnemyData", menuName = "GameData/EnemyData")]
[System.Serializable]
public class EnemyData : ScriptableObject
{
    public Sprite EnemySprite;

    public float MovementSpeed;
    public float FieldOfView;
    public float MaxHomeAwayDistance;

    public float SearchDuration = 5;
    public float AttackRate;

    public E_CharacterWorld WorldExisting;

    
}