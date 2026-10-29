using UnityEngine;

public enum GameState
{
    FreeRoam,
    Battle
}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] PokemonParty playerParty;

    // Temporaire : Pokémon sauvage fixe (sera remplacé par les hautes herbes)
    [SerializeField] PokemonBase wildBase;
    [SerializeField] int wildLevel = 5;

    GameState state;

    private void Start()
    {
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
    }

    void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        Pokemon wildPokemon = new Pokemon(wildBase, wildLevel);
        battleSystem.StartBattle(playerParty.GetHealthyPokemon(), wildPokemon);
    }

    void EndBattle(bool won)
    {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);

        // Défaite par KO : on soigne l'équipe pour éviter un GetHealthyPokemon() null
        if (playerParty.GetHealthyPokemon() == null)
        {
            foreach (Pokemon p in playerParty.Pokemons)
                p.Heal();
        }
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
    }
}
