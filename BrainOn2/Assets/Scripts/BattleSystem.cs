using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }


public class BattleSystem : MonoBehaviour
{

    public GameObject menuCombat;
    public GameObject menuInventaire;
    public GameObject menuObjet;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public TMP_Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    public BattleState state;



    
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }


    IEnumerator SetupBattle()
    {
            // Permets d'avoir les infos sur l'unit
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

            // Attends 2 secondes pour commencer
        yield return new WaitForSeconds(2f);
            // Player turn (à changer si on veut que l'enemy commence)
        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    public IEnumerator PlayerAttack(int degat, string message)
    {
            // Damage enemy et récupère if dead
        bool isDead = enemyUnit.TakeDamage(degat);

            // Update le HUD
        menuCombat.SetActive(false);
        menuInventaire.SetActive(false);
        menuObjet.SetActive(false);
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = message;         // A CHANGER selon quelle attaque

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
                // Si ennemi mort, gagné
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
                // Changement de tour à ennemi
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator EnemyTurn()
    {
        menuCombat.SetActive(false);
        menuInventaire.SetActive(false);
        menuObjet.SetActive(false);
        // Montre dans le dialogue que enemy attack
        dialogueText.text = enemyUnit.unitName + " attacks";         // A CHANGER selon quel skill cast par enemy

        yield return new WaitForSeconds(1f);

            // Player take damage
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
                // Si player mort, perdu
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
                // Changement de tour à player
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }


    void EndBattle()
    {
        enemyHUD.SetHP(enemyUnit.currentHP);

        if (state == BattleState.WON)
        {
            dialogueText.text = "Tu as gagné !";                     // A CHANGER selon l'objectif
            
                // Ajouter lien vers prochaine scène


        } else if(state == BattleState.LOST)
        {
            dialogueText.text = "You lost";

                // Ajouter lien vers restart scène
        }
    }



    void PlayerTurn()
    {
        dialogueText.text = "Choose an action :";
        menuCombat.SetActive(true);
    }

    public IEnumerator PlayerHeal(int amount, string message)
    {
        playerUnit.Heal(amount);
        playerHUD.SetHP(playerUnit.currentHP);
        menuCombat.SetActive(false);
        menuInventaire.SetActive(false);
        menuObjet.SetActive(false);
        dialogueText.text = message;

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator Search()
    {
        menuCombat.SetActive(false);
        menuInventaire.SetActive(false);
        menuObjet.SetActive(false);

        // Si plus objet dans la liste
        if (InventorySystem.Inventory.listeObjets.Count <= 0)
        {
            dialogueText.text = "Tu trouves que dalle";
            yield return new WaitForSeconds(1f);
            menuCombat.SetActive(true);
            dialogueText.text = "Choose an action :";
            yield break;
        }

            // Cherche un obj randow dans la liste des objets
        ItemSO objetSelectionne = InventorySystem.Inventory.listeObjets[Random.Range(0, InventorySystem.Inventory.listeObjets.Count)];
            // Met l'objet dans la liste d'inventaire et enleve de celle des obj
        InventorySystem.Inventory.listeInventory.Add(objetSelectionne);
        InventorySystem.Inventory.listeObjets.Remove(objetSelectionne);

        dialogueText.text = "You found " + objetSelectionne.nomObjet;
        yield return new WaitForSeconds(1f);

        

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }




    public void OnAttackButton1()
    {
            // Si ce n'est pas le tour du player, return
        if (state != BattleState.PLAYERTURN)
            return;

        // Si non, commencer le tour du player
        GlobalVar.comptePacific++;
        StartCoroutine(PlayerAttack(playerUnit.damage, "J'attaque"));
    }

    public void OnAttackButton2()
    {
        // Si ce n'est pas le tour du player, return
        if (state != BattleState.PLAYERTURN)
            return;

        // Si non, commencer le tour du player
        GlobalVar.compteChaotic++;
        StartCoroutine(PlayerAttack(playerUnit.dmgFort, "J'attaque"));
    }

    public void OnHealButton()
    {
        // Si ce n'est pas le tour du player, return
        if (state != BattleState.PLAYERTURN)
            return;

        // Si non, commencer le tour du player
        StartCoroutine(PlayerHeal(2, "Glou glou"));
    }

    public void OnSearchButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(Search());
    }

}
