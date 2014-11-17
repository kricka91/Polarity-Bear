using UnityEngine;


public class PlaceTargetWithMouse : MonoBehaviour
{

	public float surfaceOffset = 1.5f;
	public GameObject setTargetOn;
	public Transform magnetAttracting;
	public Transform magnetRepelling;
		

	// Update is called once per frame
	private void Update()
	{
	    if (Input.GetMouseButtonDown(0))
			placeMagnet(false);
		else if (Input.GetMouseButtonDown(1))
			placeMagnet(true);
	    
	}

	void placeMagnet(bool attract) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast(ray, out hit)) return;
		//            transform.position = hit.point + hit.normal*surfaceOffset;
		Instantiate(attract ? magnetAttracting : magnetRepelling, hit.point + hit.normal*surfaceOffset, transform.rotation);

		if (setTargetOn != null)
		{
			setTargetOn.SendMessage("SetTarget", this.transform);
		}
	}
}

