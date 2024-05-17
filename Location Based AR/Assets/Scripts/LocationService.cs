using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LocationService : MonoBehaviour
{
    public Text longitudeText;
    public Text latitudeText;
    public Text altitudeText;

    IEnumerator Start()
    {
        // Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services are not enabled by user.");
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            longitudeText.text = "Longitude: " + Input.location.lastData.longitude;
            latitudeText.text = "Latitude: " + Input.location.lastData.latitude;
            altitudeText.text = "Altitude: " + Input.location.lastData.altitude;
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}
