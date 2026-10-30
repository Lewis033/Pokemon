using System.Collections.Generic;
using UnityEngine;

public class PokemonParty : MonoBehaviour
{
    [SerializeField] List<PokemonData> initialPokemons;

    public List<Pokemon> Pokemons { get; private set; } = new List<Pokemon>();

    void Start()
    {
        Pokemons = new List<Pokemon>();
        foreach (var pData in initialPokemons)
        {
            Pokemons.Add(new Pokemon(pData.pokemonBase, pData.level));
        }
    }

    public Pokemon GetHealthyPokemon()
    {
        return Pokemons.Find(p => p.HP > 0);
    }

    public void AddPokemon(Pokemon newPokemon)
    {
        if (Pokemons.Count < 6)
        {
            Pokemons.Add(newPokemon);
            Debug.Log($"{newPokemon.Base.PokemonName} a été ajouté à l'équipe !");
        }
        else
        {
            Debug.Log("L'équipe est pleine !");
        }
    }
}

[System.Serializable]
public class PokemonData
{
    public PokemonBase pokemonBase;
    public int level;
}