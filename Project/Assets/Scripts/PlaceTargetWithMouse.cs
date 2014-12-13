using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

	public int maxNumMagnets = 4;
	private int curPlacedMagnets = 0;
	private List<Transform> placedMagnets = new List<Transform>();
		
	private void Start() {
//		placedMagnets = new Transform[maxNumMagnets];
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

		Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
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
			placedMagnets.Add((Transform)Instantiate(attract ? magnetAttracting : magnetRepelling, hit.transform.position, transform.rotation));
			destroyMagnet(hit.transform);
		}
		else {
			if(curPlacedMagnets > maxNumMagnets-1) {
				destroyMagnet(placedMagnets[0]);
			}
			placedMagnets.Add((Transform)Instantiate(attract ? magnetAttracting : magnetRepelling, hit.point + hit.normal*surfaceOffset, transform.rotation));
		}
		curPlacedMagnets++;
	}

	void neutralizeMagnet() {
		int layerMask = 1 << playerLayer; //hit only the player layer
		layerMask = ~layerMask; //inverse
		
		Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
		RaycastHit hit;
		if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))return; // if we hit nothing, or the player
		else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Unpolar")) return; // if we hit unpolar
		//            transform.position = hit.point + hit.normal*surfaceOffset;
		
		//layerMask = 1 << unpolarLayer; //hit only the unpolar layer
		//if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))return; // if we hit unpolar
		
		layerMask = 1 << magnetLayer; //hit only the magnet layer
		//		layerMask = ~layerMask; //inverse
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
			destroyMagnet(hit.transform);
			return;
		}
	}

	private void destroyMagnet(Transform magnet) {
		placedMagnets.Remove(magnet);
		Destroy(magnet.gameObject);
		CharacterControls character = gameObject.GetComponent<CharacterControls>();
		if (character != null) character.setAffectedByPolarity(false);
		curPlacedMagnets--;
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
			Vector3 targetPosition = hit.point + hit.normal*surfaceOffset;
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

