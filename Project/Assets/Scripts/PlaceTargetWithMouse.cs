using UnityEngine;



public class PlaceTargetWithMouse : MonoBehaviour
{

	public float surfaceOffset = 1.5f;
	public Transform magnetAttracting;
	public Transform magnetRepelling;
	public const int unpolarLayer = 8;
	public const int magnetLayer = 9;
	public const int playerLayer = 10;
		

	// Update is called once per frame
	private void Update()
	{
	    if (Input.GetMouseButtonDown(0))
			placeMagnet(false);
		else if (Input.GetMouseButtonDown(1))
			placeMagnet(true);
	    else if (Input.GetMouseButtonDown(2))
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
			return;
		}
	}
}

