using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int letterPerSecond;

    [SerializeField] Color highligthedColor;

    [SerializeField] Text dialogText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> moveTexts;

    [SerializeField] Text PPText;
    [SerializeField] Text TypeText;

    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        float timer = 0f;
        int shown = 0;

        while (shown < dialog.Length)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                shown = dialog.Length;
            }
            else
            {
                timer += Time.deltaTime;
                shown = Mathf.Min(dialog.Length, Mathf.FloorToInt(timer * letterPerSecond));
            }

            dialogText.text = dialog.Substring(0, shown);
        }

        float wait = 0f;
        while (wait < 1f)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Z))
                break;

            wait += Time.deltaTime;
        }
    }

    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; i++)
        {
            if (i == selectedAction)
            {
                actionTexts[i].color = highligthedColor;

            }
            else
            {
                actionTexts[i].color = Color.black;
            }
        }
    }

    public void UpdateMoveSelection(int selectedMove, Move move)
    {
        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i == selectedMove)
            {
                moveTexts[i].color = highligthedColor;
            }
            else
            {
                moveTexts[i].color = Color.black;
            }
        }

        PPText.text = $"PP {move.PP}/{move.Base.PP}";
        TypeText.text = move.Base.Type.ToString();
    }

    public void SetMoveNames(List<Move> moves)
    {
        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i < moves.Count)
            {
                moveTexts[i].text = moves[i].Base.MoveName;
            }
            else
            {
                moveTexts[i].text = "-";
            }
        }
    }
}
