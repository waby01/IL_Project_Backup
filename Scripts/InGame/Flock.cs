using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [Header("UI Setup")]
    public GameObject interactUiPrefab;  // Ubah ini menjadi public
    private GameObject interactUiInstance;  // Objek UI yang akan diinstansiasi

    [Header("Game Over Setup")]
    public GameOverScreen gameOverScreen; // Tambahkan referensi ke GameOverScreen

    [Header("Spawn Setup")]
    [SerializeField] private FlockUnit flockUnitPrefab;
    [SerializeField] private int flockSize;
    [SerializeField] private Vector3 spawnBounds;

    [Header("Speed Setup")]
    [Range(0, 10)]
    [SerializeField] private float _minSpeed;
    public float minSpeed { get { return _minSpeed; } }
    [Range(0, 10)]
    [SerializeField] private float _maxSpeed;
    public float maxSpeed { get { return _maxSpeed; } }

    [Header("Detection Distances")]
    [Range(0, 10)]
    [SerializeField] private float _cohesionDistance;
    public float cohesionDistance { get { return _cohesionDistance; } }

    [Range(0, 10)]
    [SerializeField] private float _avoidanceDistance;
    public float avoidanceDistance { get { return _avoidanceDistance; } }

    [Range(0, 10)]
    [SerializeField] private float _aligementDistance;
    public float aligementDistance { get { return _aligementDistance; } }

    [Range(0, 10)]
    [SerializeField] private float _obstacleDistance;
    public float obstacleDistance { get { return _obstacleDistance; } }

    [Range(0, 100)]
    [SerializeField] private float _boundsDistance;
    public float boundsDistance { get { return _boundsDistance; } }

    [Header("Behaviour Weights")]
    [Range(0, 10)]
    [SerializeField] private float _cohesionWeight;
    public float cohesionWeight { get { return _cohesionWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _avoidanceWeight;
    public float avoidanceWeight { get { return _avoidanceWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _aligementWeight;
    public float aligementWeight { get { return _aligementWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _boundsWeight;
    public float boundsWeight { get { return _boundsWeight; } }

    [Range(0, 100)]
    [SerializeField] private float _obstacleWeight;
    public float obstacleWeight { get { return _obstacleWeight; } }

    public FlockUnit[] allUnits { get; set; }

    private void Start()
    {
        GenerateUnits();

        // Instansiasi UI Interact dan matikan di awal
        if (interactUiPrefab != null)
        {
            interactUiInstance = Instantiate(interactUiPrefab);
            interactUiInstance.SetActive(false);
        }

        // Cari GameOverScreen di scene jika belum dihubungkan melalui inspector
        if (gameOverScreen == null)
        {
            gameOverScreen = FindObjectOfType<GameOverScreen>();
            if (gameOverScreen == null)
            {
                Debug.LogError("GameOverScreen not found in the scene!");
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < allUnits.Length; i++)
        {
            if (allUnits[i] != null) // Periksa apakah unit masih ada
            {
                allUnits[i].MoveUnit();
            }
        }
    }

    private void GenerateUnits()
    {
        allUnits = new FlockUnit[flockSize];
        for (int i = 0; i < flockSize; i++)
        {
            var randomVector = UnityEngine.Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
            var spawnPosition = transform.position + randomVector;
            var rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            allUnits[i] = Instantiate(flockUnitPrefab, spawnPosition, rotation);
            allUnits[i].AssignFlock(this);
            allUnits[i].InitializeSpeed(UnityEngine.Random.Range(minSpeed, maxSpeed));
        }
    }

    public void RemoveUnit(FlockUnit unit)
    {
        List<FlockUnit> unitList = new List<FlockUnit>(allUnits);
        unitList.Remove(unit);
        allUnits = unitList.ToArray();
    }

    public void TriggerGameOver(string message)
    {
        if (gameOverScreen != null)
        {
            Time.timeScale = 0; // Hentikan waktu
            gameOverScreen.setup(message); // Tampilkan pesan Game Over
        }
    }
}