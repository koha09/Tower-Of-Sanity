//  -------------------------------------------------------------------------------------------------
//  CONTRIBUTORS: Montana Games (www.montana-games.com)                  
//  Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com    
//  -------------------------------------------------------------------------------------------------

using UnityEngine;

/// <summary>
/// Характеристики Персонажа (Игрока)
/// </summary>
[CreateAssetMenu(fileName ="CharacterData",menuName ="GameData/CharacterData")]
[System.Serializable]
public class CharacterData : ScriptableObject 
{
    /// <summary>
    /// В каком мире находится персонаж сейчас
    /// </summary>
    public E_CharacterWorld CurrentWorldState { get; private set; }

    [SerializeField] private float sanity;

    public float SanityInt { get { return Mathf.RoundToInt(sanity); } }

	//TODO вынести в контроллер
    public void ChangeWorld(E_CharacterWorld world)
    {
        CurrentWorldState = world;
    }
}

[System.Serializable]
public enum E_CharacterWorld
{
    NONE=0,
    PHYSIC_WORLD=1,
    ASTRAL_WORLD=2,
    BOTH_WORLDS=3
}