using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,Character
{
    [SerializeField]
    [Range(1, 100)]
    protected float hp = 10;
    private bool combatant = false;
    [HideInInspector]
    public bool overEquipment = false;

    protected List<GameObject> weapons;

    //current weapon on hand
    protected int currentWeaponIndex = 0;

    //object currently interacting with.
    [HideInInspector]
    public GameObject interactObject;

    public GameObject hand;
    private void Start()
    {
        weapons = new List<GameObject>();
    }

    /// <summary>
    /// creates a controlled player with given hp.
    /// </summary>
    public Player(int hp)
    {
        this.hp = hp;
    }

    public void enterCombat()
    {
        this.combatant = true;
    }
    public void exitCombat()
    {
        this.combatant = false;
    }
    public bool InCombat()
    {
        return combatant;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
    }

    public void Kill(float delay)
    {
        Destroy(gameObject, delay);
    }

    public void InteractClicked()
    {
        if (overEquipment)
        {
            interactObject.gameObject.GetComponent<PickUp>().PickedUp();
            if (weapons.Count > 1)
            {
                weapons.RemoveAt(currentWeaponIndex);
            }
            weapons.Insert(currentWeaponIndex, interactObject.gameObject);
            ShowWeapon();
        }
        else
        {
            currentWeaponIndex = currentWeaponIndex + 1 > weapons.Count - 1 ? 0 : currentWeaponIndex++;
        }
    }

    void ShowWeapon()
    {
        //create the weapon on player's hand
        GameObject shown = Instantiate(weapons[currentWeaponIndex], hand.transform.position, Quaternion.identity);
        shown.transform.worldToLocalMatrix.MultiplyVector(hand.transform.forward);
        //shown.transform.LookAt(hand.transform.forward);
        shown.transform.parent = hand.transform;
    }
}
