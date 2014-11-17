using UnityEngine;


public class PlaceTargetWithMouse : MonoBehaviour
{

	public float surfaceOffset = 1.5f;
	public GameObject setTargetOn;
		public GameObject magnet;
		

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
		GameObject magnetClone = (GameObject) Instantiate(magnet, hit.point + hit.normal*surfaceOffset, transform.rotation);
		StaticObjectMagnet newMagnet = magnetClone.GetComponent<StaticObjectMagnet>();
		newMagnet.power *= attract ? -1 : 1;
		if (setTargetOn != null)
		{
			setTargetOn.SendMessage("SetTarget", this.transform);
		}
	}
}

