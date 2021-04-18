using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;

    public Animator animator;

    private Queue<string> dialogs;

    public void StartDialog(Dialog dialog)
    {
        animator.SetBool("DialogIsOpen", true);
        dialogs = new Queue<string>();
        dialogs.Clear();

        nameText.text = dialog.name;

        foreach (string sDialog in dialog.dialogs)
        {
            dialogs.Enqueue(sDialog);
        }

        DisplayNextDialog();
    }

    public bool DisplayNextDialog()
    {
        if (dialogs.Count == 0)
        {
            EndDialog();
            return true;
        }

        string dialog = dialogs.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeDialog(dialog));
        return false;
    }

    IEnumerator TypeDialog (string dialog)
    {
        dialogText.text = "";
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        animator.SetBool("DialogIsOpen", false);
    }
}
