/* Attach to game objects intended to be trigger points (hotspots). 
 * This script works in conjuction with HotSpotColorChanger.cs. */

using UnityEngine;

public class Hotspot : MonoBehaviour
{
    public delegate void HotspotEntered(GameObject someCube);
    public static event HotspotEntered OnEntered;

    public delegate void HotspotExited(GameObject someCube);
    public static event HotspotExited OnExited;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trace1", gameObject);

        if (OnEntered != null)
        {
            OnEntered(other.gameObject);

			/* Call test function */
			GameObject.Find ("GameObject_SpawnHotSpots").GetComponent<SpawnHotspots> ().testFunction ();

			/* Remove this hotspot when triggered */
			Destroy (this.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Trace2", gameObject);

        if (OnExited != null)
        {
            OnExited(other.gameObject);
        }

    }

}
