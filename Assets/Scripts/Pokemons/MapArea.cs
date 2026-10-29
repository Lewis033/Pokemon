using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WildEncounter
{
    [SerializeField] PokemonBase pokemonBase;
    [SerializeField] int minLevel = 3;
    [SerializeField] int maxLevel = 5;

    public PokemonBase Base => pokemonBase;
    public int MinLevel => minLevel;
    public int MaxLevel => maxLevel;
}

public class MapArea : MonoBehaviour
{
    [SerializeField] List<WildEncounter> wildEncounters;

    public Pokemon GetRandomWildPokemon()
    {
        WildEncounter encounter = wildEncounters[Random.Range(0, wildEncounters.Count)];
        int level = Random.Range(encounter.MinLevel, encounter.MaxLevel + 1);
        return new Pokemon(encounter.Base, level);
    }
}