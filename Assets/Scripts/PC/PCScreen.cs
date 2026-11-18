using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PCScreen : MonoBehaviour
{
    [SerializeField] Text partyText;
    [SerializeField] Text storageText;
    [SerializeField] Text messageText;

    public event Action OnClosed;

    PokemonParty party;
    bool inStorage;
    int partyIndex;
    int storageIndex;

    public void Open(PokemonParty playerParty)
    {
        party = playerParty;
        inStorage = false;
        partyIndex = 0;
        storageIndex = 0;
        messageText.text = "";
        gameObject.SetActive(true);
        Refresh();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        OnClosed?.Invoke();
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            inStorage = false;
        else if (Input.GetKeyDown(KeyCode.RightArrow) && party.Storage.Count > 0)
            inStorage = true;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            MoveCursor(1);
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            MoveCursor(-1);

        if (Input.GetKeyDown(KeyCode.Z))
            Transfer();

        if (Input.GetKeyDown(KeyCode.X))
        {
            Close();
            return;
        }

        Refresh();
    }

    void MoveCursor(int delta)
    {
        if (inStorage)
            storageIndex = Mathf.Clamp(storageIndex + delta, 0, Mathf.Max(0, party.Storage.Count - 1));
        else
            partyIndex = Mathf.Clamp(partyIndex + delta, 0, Mathf.Max(0, party.Pokemons.Count - 1));
    }

    void Transfer()
    {
        if (inStorage)
        {
            if (party.Storage.Count == 0)
                return;

            if (party.WithdrawPokemon(storageIndex))
                messageText.text = "Pokémon retiré du stockage.";
            else
                messageText.text = "L'équipe est pleine.";
        }
        else
        {
            if (party.DepositPokemon(partyIndex))
                messageText.text = "Pokémon déposé au stockage.";
            else
                messageText.text = "Il faut garder au moins un Pokémon.";
        }

        MoveCursor(0); // recadre les index si une liste a rétréci
    }

    void Refresh()
    {
        partyText.text = BuildList(party.Pokemons, partyIndex, !inStorage, "ÉQUIPE");
        storageText.text = BuildList(party.Storage, storageIndex, inStorage, "STOCKAGE");
    }

    string BuildList(List<Pokemon> list, int index, bool active, string title)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(title);

        if (list.Count == 0)
        {
            sb.AppendLine("  Aucun Pokémon");
        }

        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                string cursor = (active && i == index) ? "> " : "  ";
                sb.AppendLine($"{cursor}{list[i].Base.PokemonName} Lvl {list[i].Level}");
            }
        }

        return sb.ToString();
    }
}