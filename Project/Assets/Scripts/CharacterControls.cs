using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class CharacterControls : MonoBehaviour {

	public bool groundControlEnable = true;
	
	public float speed = 10.0f;
	public float gravity = 17.0f;
	public float maxVelocityChange = 100.0f;
	public float airMultiplier = 0.1f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	private bool grounded = false;
	bool affectedByPolarity = false;
//	public float xzAirDrag = 0.985f;
	float xzAirDrag = 1f;
	
	
	void Awake () {
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}
	
	void FixedUpdate () {
		if (grounded) {
			if(groundControlEnable && !affectedByPolarity) {
				// Calculate how fast we should be moving
				Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				targetVelocity *= speed;
				
				// Apply a force that attempts to reach our target velocity
				Vector3 velocity = rigidbody.velocity;
				targetVelocity = transform.TransformDirection(targetVelocity);

				Vector3 velocityChange = (targetVelocity - velocity);
				velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
				velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
				velocityChange.y = 0;

				rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			}

			// Jump
			if (grounded && canJump && Input.GetButton("Jump")) {
				rigidbody.velocity = new Vector3(rigidbody.velocity.x, CalculateJumpVerticalSpeed(), rigidbody.velocity.z);
			}
		} else {
			// Calculate how fast we should be moving
			Vector3 addedVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			addedVelocity *= speed * airMultiplier;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = transform.InverseTransformDirection(rigidbody.velocity);

			if(Math.Abs(velocity.x + addedVelocity.x) > Math.Abs(velocity.x) && Math.Abs(velocity.x) > speed) {
				addedVelocity.x = 0;
			} else {
				addedVelocity.x = Mathf.Clamp(addedVelocity.x, -maxVelocityChange, maxVelocityChange);
			}
			if(Math.Abs(velocity.z + addedVelocity.z) > Math.Abs(velocity.z) && Math.Abs(velocity.z) > speed) {
				addedVelocity.z = 0;
			} else {
				addedVelocity.z = Mathf.Clamp(addedVelocity.z, -maxVelocityChange, maxVelocityChange);
			}
			addedVelocity.y = 0; 		
			
			rigidbody.AddRelativeForce(addedVelocity, ForceMode.VelocityChange);

			rigidbody.AddForce(-rigidbody.velocity * xzAirDrag);

			// Jump
			if (grounded && canJump && Input.GetButton("Jump")) {
				rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}
		}
		// We apply gravity manually for more tuning control
		rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
		
		grounded = false;
	}
	
	void Update(){
		// Add air resistance
//		rigidbody.velocity = new Vector3(rigidbody.velocity.x*xzAirDrag, rigidbody.velocity.y, rigidbody.velocity.z*xzAirDrag);	
	}
	
	void OnCollisionStay (Collision collisionInfo) {
		if(collisionInfo.gameObject.tag == "Ground")
			grounded = true;
		else if(collisionInfo.gameObject.tag == "Lethal")
			Application.LoadLevel(Application.loadedLevel);
	}

	public void setAffectedByPolarity(bool affected) {
		affectedByPolarity = affected;
	}
	

	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}