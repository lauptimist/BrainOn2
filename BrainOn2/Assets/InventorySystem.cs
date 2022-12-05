using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    private ItemSO objActuel;
    public List<ItemSO> listeObjets;
    public List<ItemSO> listeInventory;
    public List<Button> listeButtons;

    public List<TextMeshProUGUI> buttonNames;

    public TextMeshProUGUI buttonAction1;
    public TextMeshProUGUI buttonAction2;

    public static InventorySystem Inventory;

    private void Awake()
    {
        if(Inventory == null)
        {
            Inventory = this;
        }
    }
    
    public void ActiverInventaire()
    {
        for(int i = 0; i < listeButtons.Count; i++)
        {
            if (i < listeInventory.Count)
            {
                listeButtons[i].gameObject.SetActive(true);
                buttonNames[i].text = listeInventory[i].nomObjet;
            }
            else
            {
                listeButtons[i].gameObject.SetActive(false);
            }
        }
    }




    public void ActiverBouton(int numeroBouton)
    {
        ActiverObjet(listeInventory[numeroBouton]);
    }

    public void ActiverObjet(ItemSO objUtilise)
    {
        buttonAction1.text = objUtilise.nomAction1;
        buttonAction2.text = objUtilise.nomAction2;

        objActuel = objUtilise;
    }

    public void ActiveAction1()
    {
        BattleSystem sys = GameObject.FindObjectOfType<BattleSystem>();
        if(objActuel.degatsAction1 > 0)
        {
            StartCoroutine(sys.PlayerAttack(objActuel.degatsAction1, objActuel.feedbackAction1));
        }
        else
        {
            StartCoroutine(sys.PlayerHeal(objActuel.soinAction1, objActuel.feedbackAction1));
        }

        if(objActuel.typeAction1 == TypeAction.PACIFIQUE)
        {
            GlobalVar.comptePacific++;
        }
        else
        {
            GlobalVar.compteChaotic++;
        }

        listeInventory.Remove(objActuel);
        objActuel = null;
    }

    public void ActiveAction2()
    {
        BattleSystem sys = GameObject.FindObjectOfType<BattleSystem>();
        if (objActuel.degatsAction2 > 0)
        {
            StartCoroutine(sys.PlayerAttack(objActuel.degatsAction2, objActuel.feedbackAction2));
        }
        else
        {
            StartCoroutine(sys.PlayerHeal(objActuel.soinAction2, objActuel.feedbackAction2));
        }

        if (objActuel.typeAction2 == TypeAction.PACIFIQUE)
        {
            GlobalVar.comptePacific++;
        }
        else
        {
            GlobalVar.compteChaotic++;
        }

        listeInventory.Remove(objActuel);
        objActuel = null;

    }


}
