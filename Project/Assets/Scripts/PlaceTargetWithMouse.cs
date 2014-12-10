﻿using UnityEngine;



public class PlaceTargetWithMouse : MonoBehaviour
{

	public float surfaceOffset = 1.5f;
	public Transform magnetAttracting;
	public Transform magnetRepelling;
	public const int unpolarLayer = 8;
	public const int magnetLayer = 9;
	public const int playerLayer = 10;
	public bool holdDownButtons = false;
	private Transform attractMagnet;
	private Transform repelMagnet;
		
	private void Start() {
		if (holdDownButtons) {
			attractMagnet = (Transform)Instantiate(magnetAttracting, Vector3.zero, Quaternion.identity);
			repelMagnet =  (Transform)Instantiate(magnetRepelling, Vector3.zero, Quaternion.identity);

			attractMagnet.gameObject.SetActive(false);
			repelMagnet.gameObject.SetActive(false);
		}
	}
	// Update is called once per frame
	private void Update()
	{
		if (holdDownButtons) {
			if (Input.GetMouseButtonDown(0))
				activateMagnet(repelMagnet);
			if (Input.GetMouseButtonDown(1))
				activateMagnet(attractMagnet);
			if (Input.GetMouseButtonUp(0))
				deactivateMagnet(repelMagnet);
			if (Input.GetMouseButtonUp(1))
				deactivateMagnet(attractMagnet);
			return;
		}
	    if (Input.GetMouseButtonDown(0))
			placeMagnet(false);
		else if (Input.GetMouseButtonDown(1))
			placeMagnet(true);
	    else if (Input.GetMouseButtonDown(2) || Input.GetKey("left ctrl") && Input.GetMouseButton(0))
			neutralizeMagnet();
	}

	void placeMagnet(bool attract) {
		int layerMask = 1 << playerLayer; //hit only the player layer
		layerMask = ~layerMask; //inverse

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))return; // if we hit nothing, or the player
		else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Unpolar")) return; // if we hit unpolar
		//            transform.position = hit.point + hit.normal*surfaceOffset;

		//layerMask = 1 << unpolarLayer; //hit only the unpolar layer
		//if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))return; // if we hit unpolar

		layerMask = 1 << magnetLayer; //hit only the magnet layer
//		layerMask = ~layerMask; //inverse
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
			Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * hit.distance, Color.yellow);
			Instantiate(attract ? magnetAttracting : magnetRepelling, hit.transform.position, transform.rotation);
			Destroy(hit.transform.gameObject);
			return;
		}
		else {

			Instantiate(attract ? magnetAttracting : magnetRepelling, hit.point + hit.normal*surfaceOffset, transform.rotation);
		}

	}

	void neutralizeMagnet() {
		int layerMask = 1 << playerLayer; //hit only the player layer
		layerMask = ~layerMask; //inverse
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))return; // if we hit nothing, or the player
		else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Unpolar")) return; // if we hit unpolar
		//            transform.position = hit.point + hit.normal*surfaceOffset;
		
		//layerMask = 1 << unpolarLayer; //hit only the unpolar layer
		//if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))return; // if we hit unpolar
		
		layerMask = 1 << magnetLayer; //hit only the magnet layer
		//		layerMask = ~layerMask; //inverse
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
			Destroy(hit.transform.gameObject);
			CharacterControls character = gameObject.GetComponent<CharacterControls>();
			if (character != null) character.setAffectedByPolarity(false);
			return;
		}
	}

	private void activateMagnet(Transform magnet) {
		
		int layerMask = 1 << playerLayer; //hit only the player layer
		layerMask = ~layerMask; //inverse
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))return; // if we hit nothing, or the player
		else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Unpolar")) return; // if we hit unpolar

		layerMask = 1 << magnetLayer; //hit only the magnet layer
		//		layerMask = ~layerMask; //inverse
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
			return;
		}
		else {
			
			magnet.gameObject.SetActive(true);
			Vector3 targetPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			magnet.position = targetPosition;
			return;
		}
	}

	private void deactivateMagnet(Transform magnet) {
		magnet.gameObject.SetActive (false);
		CharacterControls character = gameObject.GetComponent<CharacterControls>();
		if (character != null) character.setAffectedByPolarity(false);
	}
}

