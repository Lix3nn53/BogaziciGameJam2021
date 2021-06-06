using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public static WeaponInventory instance;

    public GameObject character;
    public TMP_Text ammoUI;
    public TMP_Text weaponUI;

    [SerializeField] private List<GameObject> GunList;
    int SelectedGun;
    int BombCount = 2;
    public Animator animator;

    //Melee Atack
    public float meleeAtackDelay = 0.2f;
    public float meleeAtackCd = 0.5f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public float MeleeAttackDamage = 20f;

    private bool isFiring = false;
    private bool isMeleeAttacking = false;
    private float lastFired = 10f;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        updateAmmoUI();
    }
        void Update()
    {
        lastFired += Time.deltaTime;

        if (isMeleeAttacking) return;

        Gun gun = GunList[SelectedGun].GetComponent<Gun>();
        if (lastFired > gun.cooldown && isFiring)
        {
            bool reloaded = gun.Fire();
            lastFired = 0;

            if (!reloaded)
            {
                updateAmmoUI();
            } else
            {
                StartCoroutine(ReloadCoroutine());
            }
        }
    }

    public void StartFire()
    {
        isFiring = true;
    }
    public void StopFire()
    {
        isFiring = false;
    }

    public void NextWeapon()
    {
        int nextGun;
        if (SelectedGun == GunList.Count - 1)
        {
            nextGun = 0;
        } 
        else
        {
            nextGun = SelectedGun + 1;
        }

        GunList[SelectedGun].SetActive(false);
        GunList[nextGun].SetActive(true);
        SelectedGun = nextGun;

        updateAmmoUI();
        updateWeaponUI();
    }

    public void AddWeapon(GameObject weaponToAdd)
    {
        GunList.Add(weaponToAdd);
    }

    public void ReloadGun()
    {
        if (isMeleeAttacking) return;

        Gun gun = GunList[SelectedGun].GetComponent<Gun>();

        gun.Reload();

        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(1);

        updateAmmoUI();
    }

    public void MeleeAttack()
    {
        isMeleeAttacking = true;
        GunList[SelectedGun].SetActive(false);

        animator.SetBool("MeleeAttack", true);

        StartCoroutine(MeleeAttackCoroutine());
    }

    IEnumerator MeleeAttackCoroutine()
    {
        yield return new WaitForSeconds(meleeAtackDelay);

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D hitObject in hitObjects)
        {
            //<Enemy> D��man Game Objecti ve TakeDamage hasar ald��� class
            Enemy enemy = hitObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(MeleeAttackDamage);
            }
        }

        yield return new WaitForSeconds(meleeAtackCd);

        isMeleeAttacking = false;
        GunList[SelectedGun].SetActive(true);
        animator.SetBool("MeleeAttack", false);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void updateAmmoUI()
    {
        Gun gun = GunList[SelectedGun].GetComponent<Gun>();

        ammoUI.text = gun.ammo + "/" + gun.ammoCapacity;
    }

    public float GetCurrentBulletSpeed()
    {
        Gun gun = GunList[SelectedGun].GetComponent<Gun>();

        return gun.bulletSpeed;
    }

    void updateWeaponUI()
    {
        Gun gun = GunList[SelectedGun].GetComponent<Gun>();

        weaponUI.text = gun.name;
    }
}
