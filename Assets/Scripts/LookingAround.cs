using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAround : MonoBehaviour
{
	private GameObject player;
	private float minClamp = -45;
	private float maxClamp = 45;
	[HideInInspector]
	public Vector2 rotation;
	public Vector2 currentLookRot;
	public Vector2 rotationV = new Vector2(0, 0);
	public float lookSensitivity = 5;
	public float lookSmoothDamp = 0.1f;
	// Start is called before the first frame update
	void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
		player = transform.parent.gameObject;
	}

    // Update is called once per frame
    void Update()
    {
		// player Input from the mouse

		rotation.y += Input.GetAxis("Mouse Y") * lookSensitivity;

		// limit ability to look up and down

		rotation.y = Mathf.Clamp(rotation.y, minClamp, maxClamp);

		//rotate  character around based on mouse  X position

		player.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * lookSensitivity);

		//smooth the current Y rotation for looking up and down

		currentLookRot.y = Mathf.SmoothDamp(currentLookRot.y, rotation.y, ref rotationV.y, lookSmoothDamp);

		// update camera x rotation based on values generated
		transform.localEulerAngles = new Vector3(-currentLookRot.y, 0, 0);
	}
}
