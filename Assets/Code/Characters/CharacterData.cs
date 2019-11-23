//  -------------------------------------------------------------------------------------------------
//  CONTRIBUTORS: Montana Games (www.montana-games.com)                  
//  Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com    
//  -------------------------------------------------------------------------------------------------

using UnityEngine;

/// <summary>
/// Характеристики Персонажа (Игрока)
/// </summary>
[System.Serializable]
public class CharacterData : ScriptableObject 
{
    /// <summary>
    /// В каком мире находится персонаж сейчас
    /// </summary>
    public E_CharacterWorld CurrentWorldState;

    [SerializeField] private float sanity;

    // Fields -----------------------------------------------------------------------------------
    public float SanityAbs { get { return Mathf.Abs(sanity); } }

	//  Methods ---------------------------------------------------------------------------------

}

public enum E_CharacterWorld
{
    NONE=0,
    PHYSIC_WORLD=1,
    ASTRAL_WORLD=2,
    BOTH_WORLDS=3
}