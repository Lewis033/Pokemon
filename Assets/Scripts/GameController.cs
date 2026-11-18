using UnityEngine;

public enum GameState
{
    FreeRoam,
    Battle,
    PC
}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] PokemonParty playerParty;
    [SerializeField] MapArea mapArea;
    [SerializeField] PCScreen pcScreen;

    GameState state;

    private void Start()
    {
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
        PCTerminal.OnPCOpened += OpenPC;
        pcScreen.OnClosed += ClosePC;
    }

    void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        Pokemon wildPokemon = mapArea.GetRandomWildPokemon();
        battleSystem.StartBattle(playerParty, wildPokemon);
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

        else if (state == GameState.PC)
        {
            pcScreen.HandleUpdate();
        }
    }

    private void OnDestroy()
    {
        playerController.OnEncountered -= StartBattle;
        battleSystem.OnBattleOver -= EndBattle;
        PCTerminal.OnPCOpened -= OpenPC;
        pcScreen.OnClosed -= ClosePC;
    }

    void OpenPC()
    {
        state = GameState.PC;
        pcScreen.Open(playerParty);
    }

    void ClosePC()
    {
        state = GameState.FreeRoam;
    }
}
