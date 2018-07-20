using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnHotspots : MonoBehaviour {

	/* Trigger point prefab */
	public Transform trigger_point;

	/* Global variables */
	/* Encapsulate coordinates */
	public struct CoOrds 
	{
		public float x, y, z;

		/* Constructor to initialize x, y, and z coordinates */ 
		public CoOrds(float x_coOrd, float y_coOrd, float z_coOrd)
		{
			x = x_coOrd;
			y = y_coOrd;
			z = z_coOrd;
		}
	}

	List<CoOrds> coOrds_collection = new List<CoOrds> (); 	/* Entire point collection */
	public int itr = 0;					/* Keep track of list iterations */
	public int trial = 0;					/* Keep track of completed trials */

	/* Use this for initialization */
	void Start () {
		initializeCoordinates (ref coOrds_collection);

		/* Call function once on startup to create initial hotspot */
		HotSpotTriggerInstantiate ();
	}

	/* Set up the entire point collectin list.
	 * Parameter is the global list variable containing the entire coordinate collection.
	 * At the start it is empty and after this function finishes it is filled
	 * with all the coordinates in a randomized order. */
	public void initializeCoordinates (ref List<CoOrds> coOrds_collection)
	{

		/* Useful variable and data structure definitions */
		CoOrds coords_temp = new CoOrds (); 				

		/* Create all the data points and add them to the list */
		/* z = 0 frame				 (x, y, z) */
		CoOrds coords_1 = new CoOrds (0, 0, 0);
		coOrds_collection.Add (coords_1);
		CoOrds coords_2 = new CoOrds (0.3f, 0, 0);
		coOrds_collection.Add (coords_2);
		CoOrds coords_3 = new CoOrds (0.6f, 0, 0);
		coOrds_collection.Add (coords_3);
		CoOrds coords_4 = new CoOrds (0, 0.3f, 0);
		coOrds_collection.Add (coords_4);
		CoOrds coords_5 = new CoOrds (0.3f, 0.3f, 0);
		coOrds_collection.Add (coords_5);
		CoOrds coords_6 = new CoOrds (0.6f, 0.3f, 0);
		coOrds_collection.Add (coords_6);
		CoOrds coords_7 = new CoOrds (0, 0.6f, 0);
		coOrds_collection.Add (coords_7);
		CoOrds coords_8 = new CoOrds (0.3f, 0.6f, 0);
		coOrds_collection.Add (coords_8);
		CoOrds coords_9 = new CoOrds (0.6f, 0.6f, 0);
		coOrds_collection.Add (coords_9);

		/* z = 0.3 frame			  (x, y, z) */
		CoOrds coords_10 = new CoOrds (0, 0, 0.3f);
		coOrds_collection.Add (coords_10);
		CoOrds coords_11 = new CoOrds (0.3f, 0, 0.3f);
		coOrds_collection.Add (coords_11);
		CoOrds coords_12 = new CoOrds (0.6f, 0, 0.3f);
		coOrds_collection.Add (coords_12);
		CoOrds coords_13 = new CoOrds (0, 0.3f, 0.3f);
		coOrds_collection.Add (coords_13);
		CoOrds coords_14 = new CoOrds (0.3f, 0.3f, 0.3f);
		coOrds_collection.Add (coords_14);
		CoOrds coords_15 = new CoOrds (0.6f, 0.3f, 0.3f);
		coOrds_collection.Add (coords_15);
		CoOrds coords_16 = new CoOrds (0, 0.6f, 0.3f);
		coOrds_collection.Add (coords_16);
		CoOrds coords_17 = new CoOrds (0.3f, 0.6f, 0.3f);
		coOrds_collection.Add (coords_17);
		CoOrds coords_18 = new CoOrds (0.6f, 0.6f, 0.3f);
		coOrds_collection.Add (coords_18);

		/* z = 0.6 frame			  (x, y, z) */
		CoOrds coords_19 = new CoOrds (0, 0, 0.6f);
		coOrds_collection.Add (coords_19);
		CoOrds coords_20 = new CoOrds (0.3f, 0, 0.6f);
		coOrds_collection.Add (coords_20);
		CoOrds coords_21 = new CoOrds (0.6f, 0, 0.6f);
		coOrds_collection.Add (coords_21);
		CoOrds coords_22 = new CoOrds (0, 0.3f, 0.6f);
		coOrds_collection.Add (coords_22);
		CoOrds coords_23 = new CoOrds (0.3f, 0.3f, 0.6f);
		coOrds_collection.Add (coords_23);
		CoOrds coords_24 = new CoOrds (0.6f, 0.3f, 0.6f);
		coOrds_collection.Add (coords_24);
		CoOrds coords_25 = new CoOrds (0, 0.6f, 0.6f);
		coOrds_collection.Add (coords_25);
		CoOrds coords_26 = new CoOrds (0.3f, 0.6f, 0.6f);
		coOrds_collection.Add (coords_26);
		CoOrds coords_27 = new CoOrds (0.6f, 0.6f, 0.6f);
		coOrds_collection.Add (coords_27);

		/* Shuffle list to randomize spawn order */
		if (itr == 0) {
			shuffle (ref coOrds_collection);
		}

		return;

	}

	/* Shuffles a list. In this context it shuffles the coordinate collection list. */
	public void shuffle (ref List<CoOrds> coOrds_collection)
	{

		/* Fisher Yates shuffle list to randomize spawn order */
		Debug.Log ("Shuffling hotspot spawn points!");

		CoOrds coords_temp = new CoOrds (); 				
		int random_placeholder;

		for (int i = 0; i < coOrds_collection.Count; i++) {
			random_placeholder = i + Random.Range (0, coOrds_collection.Count - i);

			/* Swap */
			coords_temp = coOrds_collection [i];
			coOrds_collection [i] = coOrds_collection [random_placeholder];
			coOrds_collection [random_placeholder] = coords_temp;
		}

	}

	/* Resets the task for a new trial by
	 * destroying all the triggered cubes,
	 * resetting itr, and shuffling the trigger
	 * point spawn coordinates. */
	public void reset ()
	{
		Debug.Log ("Inside reset()");

		/* Destroy triggered cubes */
		GameObject[] triggeredCubes = GameObject.FindGameObjectsWithTag ("triggered");

		for(var i = 0; i < triggeredCubes.Length; i++)
		{
			Destroy(triggeredCubes[i]);
		}

		itr = 0;

		shuffle (ref coOrds_collection);

	}
		
	/* This function is called from Hotspot.cs after Start () */
	public void HotSpotTriggerInstantiate ()
	{
		CoOrds coords_temp = new CoOrds (); 				

		/* Begin spawning */
		if (itr < coOrds_collection.Count) {
			Debug.Log ("coOrds_collection count: " + coOrds_collection.Count + " itr: " + itr);
			
			/* Copy the next coordinate in the list to the temp variable */
			coords_temp = coOrds_collection [itr];
			itr++;

			/* Spawn the point */ 
			Instantiate (trigger_point, new Vector3 (coords_temp.x, coords_temp.y, coords_temp.z), Quaternion.identity);

			/* Debugging */
			if (itr == coOrds_collection.Count) {
				Debug.Log ("Entire Coords_Collection spawned!");
				Debug.Log ("coOrds_collection count: " + coOrds_collection.Count + " itr: " + itr);
			}

		}
		/* Start new trial */
		else 
		{
			trial++; Debug.Log ("trial = " + trial + " completed!");

			//TODO spawn counter value object in view/scene

			if(trial < 3)
			{
				Debug.Log( "Starting a new trial!" );
				reset();
			}

		}

		return;

	}

}
