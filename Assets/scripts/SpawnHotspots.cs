using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using System.Threading;

public class SpawnHotspots : MonoBehaviour {

	/* Prefabs */
	public Transform trigger_point;
	public Transform trial_counter;
	public Transform cubes;

	/* Global variables */
	/* Encapsulate coordinates */
	public struct CoOrds 
	{
		public float x, y, z;
		public string plane;

		/* Constructor to initialize x, y, and z coordinates */ 
		public CoOrds(float x_coOrd, float y_coOrd, float z_coOrd, string p)
		{
			x = x_coOrd;
			y = y_coOrd;
			z = z_coOrd;
			plane = p;
		}
	}

	List<CoOrds> coOrds_collection = new List<CoOrds> (); 	/* Entire point collection */
    	List<CoOrds> counter_collection = new List<CoOrds> ();	/* Trial counter coordinates */
	public int itr = 0;					/* Keep track of list iterations */
	public int trial = 0;					/* Keep track of completed trials */

	public string fileName = "cube_task_time_";
	public Stopwatch stopwatch = new Stopwatch();
	public string path;

	/* Use this for initialization */
	void Start () 
	{

		// Create unique out file 
		fileName = fileName + System.DateTime.Now + ".txt";
		fileName = fileName.Replace("/","-");
		fileName = fileName.Replace(":",";");
		path = Path.Combine(Application.persistentDataPath, fileName);
		//Test outfile
		//File.WriteAllText(@path, "trace");

		initializeCoordinates (ref coOrds_collection, ref counter_collection);

		/* Call function once on startup to create initial hotspot */
		HotSpotTriggerInstantiate ();

		/* Spawn new cube collection */
		Instantiate (cubes, new Vector3 (-0.3f, 0.3f, 0.3f), Quaternion.identity, this.transform); // Make this gameObject the parent
	}

	/* Populate point collection and counter collection coordinate lists.
	 * Parameters are global list variables referencing the coordinate collections.
	 * At the start it is empty and after this function finishes it is filled
	 * with all the trigger point coordinates in a randomized order and trial. 
	 * counter coordinates. */
	public void initializeCoordinates (ref List<CoOrds> coOrds_collection, ref List<CoOrds> counter_collection)
	{

		/* Create all the data points and add them to the list */
		/* z = 0 frame				 (x, y, z) */
		CoOrds coords_1 = new CoOrds (0, 0, 0, "front");
		coOrds_collection.Add (coords_1);
		CoOrds coords_2 = new CoOrds (0.3f, 0, 0, "front");
		coOrds_collection.Add (coords_2);
		CoOrds coords_3 = new CoOrds (0.6f, 0, 0, "front");
		coOrds_collection.Add (coords_3);
		CoOrds coords_4 = new CoOrds (0, 0.3f, 0, "front");
		coOrds_collection.Add (coords_4);
		CoOrds coords_5 = new CoOrds (0.3f, 0.3f, 0, "front");
		coOrds_collection.Add (coords_5);
		CoOrds coords_6 = new CoOrds (0.6f, 0.3f, 0, "front");
		coOrds_collection.Add (coords_6);
		CoOrds coords_7 = new CoOrds (0, 0.6f, 0, "front");
		coOrds_collection.Add (coords_7);
		CoOrds coords_8 = new CoOrds (0.3f, 0.6f, 0, "front");
		coOrds_collection.Add (coords_8);
		CoOrds coords_9 = new CoOrds (0.6f, 0.6f, 0, "front");
		coOrds_collection.Add (coords_9);

		/* z = 0.3 frame			  (x, y, z) */
		CoOrds coords_10 = new CoOrds (0, 0, 0.3f, "middle");
		coOrds_collection.Add (coords_10);
		CoOrds coords_11 = new CoOrds (0.3f, 0, 0.3f, "middle");
		coOrds_collection.Add (coords_11);
		CoOrds coords_12 = new CoOrds (0.6f, 0, 0.3f, "middle");
		coOrds_collection.Add (coords_12);
		CoOrds coords_13 = new CoOrds (0, 0.3f, 0.3f, "middle");
		coOrds_collection.Add (coords_13);
		CoOrds coords_14 = new CoOrds (0.3f, 0.3f, 0.3f, "middle");
		coOrds_collection.Add (coords_14);
		CoOrds coords_15 = new CoOrds (0.6f, 0.3f, 0.3f, "middle");
		coOrds_collection.Add (coords_15);
		CoOrds coords_16 = new CoOrds (0, 0.6f, 0.3f, "middle");
		coOrds_collection.Add (coords_16);
		CoOrds coords_17 = new CoOrds (0.3f, 0.6f, 0.3f, "middle");
		coOrds_collection.Add (coords_17);
		CoOrds coords_18 = new CoOrds (0.6f, 0.6f, 0.3f, "middle");
		coOrds_collection.Add (coords_18);

		/* z = 0.6 frame			  (x, y, z) */
		CoOrds coords_19 = new CoOrds (0, 0, 0.6f, "back");
		coOrds_collection.Add (coords_19);
		CoOrds coords_20 = new CoOrds (0.3f, 0, 0.6f, "back");
		coOrds_collection.Add (coords_20);
		CoOrds coords_21 = new CoOrds (0.6f, 0, 0.6f, "back");
		coOrds_collection.Add (coords_21);
		CoOrds coords_22 = new CoOrds (0, 0.3f, 0.6f, "back");
		coOrds_collection.Add (coords_22);
		CoOrds coords_23 = new CoOrds (0.3f, 0.3f, 0.6f, "back");
		coOrds_collection.Add (coords_23);
		CoOrds coords_24 = new CoOrds (0.6f, 0.3f, 0.6f, "back");
		coOrds_collection.Add (coords_24);
		CoOrds coords_25 = new CoOrds (0, 0.6f, 0.6f, "back");
		coOrds_collection.Add (coords_25);
		CoOrds coords_26 = new CoOrds (0.3f, 0.6f, 0.6f, "back");
		coOrds_collection.Add (coords_26);
		CoOrds coords_27 = new CoOrds (0.6f, 0.6f, 0.6f, "back");
		coOrds_collection.Add (coords_27);

		/* Shuffle list to randomize spawn order */
		shuffle (ref coOrds_collection);

		/* Trial counters */
		CoOrds counter_1 = new CoOrds (-0.43f, 0.57f, 0.3f, null);
		counter_collection.Add (counter_1);
		CoOrds counter_2 = new CoOrds (-0.37f, 0.57f, 0.3f, null);
		counter_collection.Add (counter_2);
		CoOrds counter_3 = new CoOrds (-0.31f, 0.57f, 0.3f, null);
		counter_collection.Add (counter_3);
	}

	/* Shuffle a coordinate list. */
	public void shuffle (ref List<CoOrds> coOrds_collection)
	{

		/* Fisher Yates shuffle list to randomize spawn order */
		UnityEngine.Debug.Log ("Shuffling hotspot spawn points!");

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

	/* Reset the task for a new trial by
	 * destroying all the triggered cubes,
	 * spawning a new cube collection,
	 * resetting itr, and shuffling the trigger
	 * point spawn coordinates. */
	public void reset ()
	{

		/* Destroy triggered cubes */
		GameObject[] triggeredCubes = GameObject.FindGameObjectsWithTag ("triggered");

		for(var i = 0; i < triggeredCubes.Length; i++) {
			Destroy(triggeredCubes[i]);
		}

		/* Spawn new cube collection */
		Transform local_cubes = Instantiate (cubes, new Vector3 (-0.3f, 0.3f, 0.3f), Quaternion.identity, this.transform); // Make this gameObject the parent
		local_cubes.localPosition = new Vector3 (-0.3f, 0.3f, 0.3f); // Spawn position relative to parent

		itr = 0;

		shuffle (ref coOrds_collection);
		HotSpotTriggerInstantiate ();

	}
		
	/* This function is called from Hotspot.cs after Start () */
	public void HotSpotTriggerInstantiate ()
	{
		
		/* check if user has tapped first point */
		if (itr == 1) {
			// Begin timing
			stopwatch.Start();
		}

		CoOrds coords_temp = new CoOrds (); 				

		/* Begin spawning */
		if (itr < coOrds_collection.Count) {
			UnityEngine.Debug.Log ("coOrds_collection count: " + coOrds_collection.Count + " itr: " + itr);
			
			/* Copy the next coordinate in the list to the temp variable */
			coords_temp = coOrds_collection [itr];
			itr++;

			/* Spawn the point */ 
			Transform local_trigger_point = Instantiate (trigger_point, new Vector3 (coords_temp.x, coords_temp.y, coords_temp.z), Quaternion.identity, this.transform); // Make this gameObject the parent

			switch (coords_temp.plane) {
				case "front":
					local_trigger_point.tag = "front";
					break;
				case "middle":
					local_trigger_point.tag = "middle";
					break;
				case "back":
					local_trigger_point.tag = "back";
					break;
			}

			local_trigger_point.localPosition = new Vector3 (coords_temp.x, coords_temp.y, coords_temp.z); // Spawn position relative to parent 

			/* UnityEngine.Debugging */
			if (itr == coOrds_collection.Count) {
				UnityEngine.Debug.Log ("Entire Coords_Collection spawned!");
				UnityEngine.Debug.Log ("coOrds_collection count: " + coOrds_collection.Count + " itr: " + itr);
			}

		}
		/* Start new trial and update counter */
		else {
			UnityEngine.Debug.Log( "Starting a new trial!" );

			/* Copy counter location coordinates */
			coords_temp = counter_collection [trial];
			trial++; 

			// Stop timing
			System.TimeSpan ts = stopwatch.Elapsed;
			stopwatch.Stop();
			UnityEngine.Debug.Log("Time elapsed: " + ts);
			stopwatch.Reset();

			// Write time to file
			File.AppendAllText(@path, "Trial " + trial + " : ");
			File.AppendAllText(@path, ts.ToString());
			File.AppendAllText(@path, "\r\n");

			/* Spawn counter */
			Transform local_trial_counter = Instantiate (trial_counter, new Vector3 (coords_temp.x, coords_temp.y, coords_temp.z), Quaternion.identity, this.transform); // Make this gameObject the parent
			local_trial_counter.localPosition  = new Vector3 (coords_temp.x, coords_temp.y, coords_temp.z); // Spawn position relative to parent 

			UnityEngine.Debug.Log( "Trial " + trial + " completed!");

			if (trial < 3) {
				reset();
			}
				
		}

		return;

	}

}
