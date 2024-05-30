using System.Collections;
using UnityEngine;
using TMPro;

public class LocationService : MonoBehaviour
{
    public TMP_Text longitudeText;
    public TMP_Text latitudeText;
    public TMP_Text altitudeText;
    public TMP_Text storedCoordinatesText;
    public TMP_Text distanceText;
    public CubeSpawner cubeSpawner;

    private Vector3 storedCoordinates;

    void Start()
    {
        StartCoroutine(StartLocationService());
    }

    IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            yield break;
        }
        else
        {
            StartCoroutine(UpdateLocation());
        }
    }

    IEnumerator UpdateLocation()
    {
        while (true)
        {
            if (Input.location.status == LocationServiceStatus.Running)
            {
                float longitude = Input.location.lastData.longitude;
                float latitude = Input.location.lastData.latitude;
                float altitude = Input.location.lastData.altitude;

                longitudeText.text = "Longitude: " + longitude;
                latitudeText.text = "Latitude: " + latitude;
                altitudeText.text = "Altitude: " + altitude;
            }

            yield return new WaitForSeconds(0.5f); // Update every 0.5 seconds
        }
    }

    public void StoreCoordinates()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            storedCoordinates = new Vector3(Input.location.lastData.latitude, Input.location.lastData.longitude, Input.location.lastData.altitude);
            storedCoordinatesText.text = "Stored Coordinates: Lat " + storedCoordinates.x + ", Lon " + storedCoordinates.y;
        }
    }

    public void CalculateDistance()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            Vector3 currentCoordinates = new Vector3(Input.location.lastData.latitude, Input.location.lastData.longitude, Input.location.lastData.altitude);
            float distance = Vector3.Distance(storedCoordinates, currentCoordinates) * 1000; // Distance in meters
            distanceText.text = "Distance: " + Mathf.Round(distance * 100f) / 100f + " meters";
        }
    }

    public void DrawObject()
    {
        cubeSpawner.SpawnCube();
    }
}
