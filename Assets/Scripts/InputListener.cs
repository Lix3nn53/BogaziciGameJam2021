using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListener : MonoBehaviour
{
	public CharacterController2D controller;
	public Animator animator;
	public Transform camTarget;

	public WeaponInventory weaponInventory;

	public float runSpeed = 40f;
	public float lookDownOffsetPer = 2f;
	public float lookDownOffsetMin = -4f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	// Update is called once per frame
	void Update()
	{

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}


		Vector3 localPosition = camTarget.transform.localPosition;

		if (crouch)
		{

			if (localPosition.y > lookDownOffsetMin)
			{
				camTarget.transform.localPosition = new Vector3(localPosition.x, camTarget.transform.localPosition.y - lookDownOffsetPer * Time.deltaTime, localPosition.z);
			}
		}
		else
		{
			if (localPosition.y < 0)
			{
				camTarget.transform.localPosition = new Vector3(localPosition.x, camTarget.transform.localPosition.y + lookDownOffsetPer * Time.deltaTime, localPosition.z);
			}
		}


		/*if (Input.GetButtonDown("NextWeapon"))
		{
			weaponInventory.nextWeapon();
		}*/

		if (Input.GetButton("Fire1"))
		{
			weaponInventory.StartFire();
		}
		else if (Input.GetButtonUp("Fire1"))
		{
			weaponInventory.StopFire();
		}

		if (Input.GetButtonDown("MeleeAttack"))
		{
			weaponInventory.MeleeAttack();
		}

		if (Input.GetButtonDown("NextWeapon"))
		{
			weaponInventory.NextWeapon();
		}

		if (Input.GetButtonDown("Reload"))
		{
			weaponInventory.ReloadGun();
		}

		/*if (Input.GetButtonDown("Interract"))
        {
			Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, 20f, interactableLayer);

			foreach (Collider2D hitObject in hitObjects)
			{
				//<Enemy> D��man Game Objecti ve TakeDamage hasar ald��� class
				InteractableScript interactable = hitObject.GetComponent<InteractableScript>();
				if (interactable != null)
				{
					interactable.OnInterract();
				}
			}
		}*/
	}

	public void OnLanding()
	{

		Debug.Log(string.Format("landed!"));
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching(bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		jump = false;
	}
}
