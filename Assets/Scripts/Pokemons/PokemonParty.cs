using System.Collections.Generic;
using UnityEngine;

public class PokemonParty : MonoBehaviour
{
    [SerializeField] PokemonBase starterBase;
    [SerializeField] int starterLevel = 5;

    public List<Pokemon> Pokemons { get; private set; } = new List<Pokemon>();

    void Start()
    {
        Pokemons.Add(new Pokemon(starterBase, starterLevel));
    }

    public Pokemon GetHealthyPokemon()
    {
        return Pokemons.Find(p => p.HP > 0);
    }
}