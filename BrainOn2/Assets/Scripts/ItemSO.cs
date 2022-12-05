using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objet", menuName = "Objet", order = 1)]
public class ItemSO : ScriptableObject
{
    public string nomObjet;

    [Header("Action 1")]
    public string nomAction1;
    public TypeAction typeAction1;
    public string feedbackAction1;
    public int soinAction1;
    public int degatsAction1;

    [Header("Action 2")]
    public string nomAction2;
    public TypeAction typeAction2;
    public string feedbackAction2;
    public int soinAction2;
    public int degatsAction2;

    public ItemSO GetItem()
    {
        return this;
    }


}

public enum TypeAction
{
    PACIFIQUE,
    CHAOTIQUE
}

