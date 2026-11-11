using System;
using System.Collections;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy, PartyScreen }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] PartyScreen partyScreen;

    public event Action<bool> OnBattleOver;

    BattleState state;
    int currentAction;
    int currentMove;
    int currentMember;

    PokemonParty playerParty;
    Pokemon wildPokemon;

    public void StartBattle(PokemonParty playerParty, Pokemon wildPokemon)
    {
        this.playerParty = playerParty;
        this.wildPokemon = wildPokemon;
        StartCoroutine(SetupBattle());
    }

    public void HandleUpdate()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
        else if (state == BattleState.PartyScreen)
        {
            HandlePartySelection();
        }
    }

    public IEnumerator SetupBattle()
    {
        currentAction = 0;
        currentMove = 0;

        playerUnit.Setup(playerParty.GetHealthyPokemon());
        enemyUnit.Setup(wildPokemon);
        playerHud.SetData(playerUnit.Pokemon);
        enemyHud.SetData(enemyUnit.Pokemon);

        partyScreen.Init();

        dialogBox.SetMoveNames(playerUnit.Pokemon.Moves);

        yield return dialogBox.TypeDialog($"Un {enemyUnit.Pokemon.Base.PokemonName} sauvage apparait!");

        PlayerAction();
    }

    void PlayerAction()
    {
        StartCoroutine(PlayerActionRoutine());
    }

    void OpenPartyScreen()
    {
        state = BattleState.PartyScreen;
        partyScreen.SetPartyData(playerParty.Pokemons);
        partyScreen.gameObject.SetActive(true);
    }

    IEnumerator PlayerActionRoutine()
    {
        state = BattleState.Busy;
        yield return dialogBox.TypeDialog("Choisir une action");
        dialogBox.EnableActionSelector(true);
        state = BattleState.PlayerAction;
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;

        Move move = playerUnit.Pokemon.Moves[currentMove];
        move.PP--;

        yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.PokemonName} utilise {move.Base.MoveName}!");

        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        enemyUnit.PlayHitAnimation();

        DamageDetails damageDetails = enemyUnit.Pokemon.TakeDamage(move, playerUnit.Pokemon);
        yield return enemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Pokemon.Base.PokemonName} est KO.");
            enemyUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);
            OnBattleOver?.Invoke(true);
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        Move move = enemyUnit.Pokemon.GetRandomMove();
        move.PP = Mathf.Max(0, move.PP - 1);

        yield return dialogBox.TypeDialog($"{enemyUnit.Pokemon.Base.PokemonName} utilise {move.Base.MoveName}!");

        enemyUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        playerUnit.PlayHitAnimation();

        DamageDetails damageDetails = playerUnit.Pokemon.TakeDamage(move, enemyUnit.Pokemon);
        yield return playerHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.PokemonName} est KO.");
            playerUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);

            Pokemon nextPokemon = playerParty.GetHealthyPokemon();
            if (nextPokemon is not null)
            {
                OpenPartyScreen();
            }
            else
            {
                OnBattleOver?.Invoke(false);
            }
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
        {
            yield return dialogBox.TypeDialog("C'est un coup critique!");
        }

        if (damageDetails.TypeEffectiveness > 1f)
        {

            yield return dialogBox.TypeDialog("C'est super efficace!");
        }

        else if (damageDetails.TypeEffectiveness < 1f)
        {
            yield return dialogBox.TypeDialog("Ce n'est pas très efficace...");
        }
            
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentAction++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentAction--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentAction += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentAction -= 2;
        }

        currentAction = Mathf.Clamp(currentAction, 0, 3);

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                // Combat
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                // Sac
                StartCoroutine(ThrowPokeball());
            }
            else if (currentAction == 2)
            {
                // Pokemon
                OpenPartyScreen();
            }
            else if (currentAction == 3)
            {
                // Fuite
                StartCoroutine(RunAway());
            }
        }
    }

    IEnumerator RunAway()
    {
        state = BattleState.Busy;
        dialogBox.EnableActionSelector(false);

        yield return dialogBox.TypeDialog("Vous prenez la fuite!");
        yield return new WaitForSeconds(1f);

        OnBattleOver?.Invoke(false);
    }

    IEnumerator ThrowPokeball()
    {
        state = BattleState.Busy;
        dialogBox.EnableActionSelector(false);

        Pokemon wild = enemyUnit.Pokemon;

        yield return dialogBox.TypeDialog("Vous lancez une Pokéball!");

        if (TryCatch(wild))
        {
            bool inParty = playerParty.AddPokemon(wild);

            yield return dialogBox.TypeDialog($"{wild.Base.PokemonName} est capturé!");
            yield return dialogBox.TypeDialog(inParty
                ? $"{wild.Base.PokemonName} rejoint votre équipe!"
                : $"{wild.Base.PokemonName} est envoyé au stockage.");

            OnBattleOver?.Invoke(true);
        }
        else
        {
            yield return dialogBox.TypeDialog($"Oh non! {wild.Base.PokemonName} s'est libéré!");
            StartCoroutine(EnemyMove());
        }
    }

    bool TryCatch(Pokemon wild)
    {
        float hpRatio = (float)wild.HP / wild.MaxHp;
        float chance = (1f - (2f / 3f) * hpRatio) * (wild.Base.CatchRate / 255f);
        return UnityEngine.Random.value <= chance;
    }

    void HandleMoveSelection()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentMove++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentMove--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentMove += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentMove -= 2;
        }

        currentMove = Mathf.Clamp(currentMove, 0, playerUnit.Pokemon.Moves.Count - 1);

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Pokemon.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Move selectedMove = playerUnit.Pokemon.Moves[currentMove];
            if (selectedMove.PP == 0)
                return;

            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            PlayerAction();
        }
    }

    void HandlePartySelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentMember++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentMember--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentMember += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentMember -= 2;
        }

        currentMember = Mathf.Clamp(currentMember, 0, playerParty.Pokemons.Count - 1);

        partyScreen.UpdateMemberSelection(currentMember);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Pokemon selectedMember = playerParty.Pokemons[currentMember];
            if (selectedMember.HP <= 0)
            {
                partyScreen.SetMessageText("Vous ne pouvez pas envoyer un Pokémon KO");
                return;
            }
            if (selectedMember == playerUnit.Pokemon)
            {
                partyScreen.SetMessageText("Vous ne pouvez pas changer avec le même Pokémon");
                return;
            }

            partyScreen.gameObject.SetActive(false);
            state = BattleState.Busy;
            StartCoroutine(SwitchPokemon(selectedMember));
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (playerUnit.Pokemon.HP <= 0)
            {
                partyScreen.SetMessageText("Vous devez choisir un Pokémon pour continuer");
                return;
            }
            partyScreen.gameObject.SetActive(false);
            PlayerAction();
        }
    }
    IEnumerator SwitchPokemon(Pokemon newPokemon)
    {
        dialogBox.EnableActionSelector(false);

        bool wasFainted = playerUnit.Pokemon.HP <= 0;

        if (playerUnit.Pokemon.HP > 0)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.PokemonName}, reviens !");
            playerUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(2f);
        }

        playerUnit.Setup(newPokemon);
        playerHud.SetData(newPokemon);
        dialogBox.SetMoveNames(newPokemon.Moves);

        yield return dialogBox.TypeDialog($"Va ! {newPokemon.Base.PokemonName} !");

        if (wasFainted)
            PlayerAction();
        else
            StartCoroutine(EnemyMove());
    }
}
