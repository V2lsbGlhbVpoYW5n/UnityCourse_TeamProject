using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = System.Random;
using Math = System.Math;

public class ComputerUnitsManagerLogic : UnitsManagerLogic
{
    [SerializeField] public GameObject RallyPos;
    [SerializeField] public GameObject FirstLayerDest1;
    [SerializeField] public GameObject FirstLayerDest2;
    [SerializeField] public GameObject FirstLayerDest3;
    [SerializeField] public GameObject SecondLayerDest1;
    [SerializeField] public GameObject SecondLayerDest2;
    [SerializeField] public GameObject SecondLayerDest3;
    [SerializeField] public GameObject SecondLayerDest4;
    [SerializeField] public GameObject FinalDest;

    private List<KeyValuePair<Transform, int>> PosList;

    // Start is called before the first frame update
    void Start()
    {
        PosList = new List<KeyValuePair<Transform, int>>();
        PosList.Add(new KeyValuePair<Transform, int>(RallyPos.transform, 0));
        PosList.Add(new KeyValuePair<Transform, int>(FirstLayerDest1.transform, 1));
        PosList.Add(new KeyValuePair<Transform, int>(FirstLayerDest2.transform, 1));
        PosList.Add(new KeyValuePair<Transform, int>(FirstLayerDest3.transform, 1));
        PosList.Add(new KeyValuePair<Transform, int>(SecondLayerDest1.transform, 2));
        PosList.Add(new KeyValuePair<Transform, int>(SecondLayerDest2.transform, 2));
        PosList.Add(new KeyValuePair<Transform, int>(SecondLayerDest3.transform, 2));
        PosList.Add(new KeyValuePair<Transform, int>(SecondLayerDest4.transform, 2));
        PosList.Add(new KeyValuePair<Transform, int>(FinalDest.transform, 3));

        warriors = GameObject.FindGameObjectsWithTag("ComputerWarrior");
        magicians = GameObject.FindGameObjectsWithTag("ComputerMagician");
        shooters = GameObject.FindGameObjectsWithTag("ComputerShooter");
    }

    // Update is called once per frame
    void Update()
    {
        if (warriors != null)
        {
            foreach (var warrior in warriors)
            {
                KeyValuePair<Transform, int> currentDest = CheckCurrentDest(warrior);
                if (currentDest.Key != FinalDest)
                {
                    if (currentDest.Value == -1)
                    {
                        SetInitDest(warrior);
                    }
                    else if (CalculateDestDist(warrior) < 0.5)
                    {
                        SetNextDest(warrior, currentDest.Value + 1);
                    }
                }
            }
        }

        if (shooters != null)
        {
            foreach (var shooter in shooters)
            {
                KeyValuePair<Transform, int> currentDest = CheckCurrentDest(shooter);
                if (currentDest.Key != FinalDest)
                {
                    if (currentDest.Value == -1)
                    {
                        SetInitDest(shooter);
                    }
                    else if (CalculateDestDist(shooter) < 0.5)
                    {
                        SetNextDest(shooter, currentDest.Value + 1);
                    }
                }
            }
        }

        if (magicians != null)
        {
            foreach (var magician in magicians)
            {
                KeyValuePair<Transform, int> currentDest = CheckCurrentDest(magician);
                if (currentDest.Key != FinalDest)
                {
                    if (currentDest.Value == -1)
                    {
                        SetInitDest(magician);
                    }
                    else if (CalculateDestDist(magician) < 0.5)
                    {
                        SetNextDest(magician, currentDest.Value + 1);
                    }
                }
            }
        }

    }

    private void LateUpdate()
    {
        warriors = GameObject.FindGameObjectsWithTag("ComputerWarrior");
        magicians = GameObject.FindGameObjectsWithTag("ComputerMagician");
        shooters = GameObject.FindGameObjectsWithTag("ComputerShooter");
    }

    private float CalculateDestDist(GameObject gameObject)
    {
        Vector3 destination = gameObject.GetComponent<UnitLogic>().GetMovementDest();
        Vector3 pos = gameObject.transform.position;
        return Math.Abs(destination.x - pos.x) + Math.Abs(destination.z - pos.z);
    }
    
    private KeyValuePair<Transform, int> CheckCurrentDest(GameObject gameObject)
    {
        gameObject.TryGetComponent(out UnitLogic unitLogic);
        if (unitLogic != null)
        {
            Vector3 destination = unitLogic.GetMovementDest();
            foreach (var pair in PosList)
            {
                if (pair.Key.position == destination)
                {
                    return pair;
                }
            }
        }
        return new KeyValuePair<Transform, int>(null, -1);
    }

    private List<Transform> FindPosByLayer(int layer)
    {
        List<Transform> poss = new List<Transform>();
        foreach (var pair in PosList)
        {
            if (pair.Value == layer)
            {
                poss.Add(pair.Key);
            }
        }

        return poss;
    }

    private Transform GetRandomPosWithLayer(int layer)
    {
        List<Transform> poss = FindPosByLayer(layer);
        Random random = new Random();
        return poss[random.Next(poss.Count)];
    }

    private void SetNextDest(GameObject gameObject, int nextLayer)
    {
        KeyValuePair<Transform, int> keyValuePair = CheckCurrentDest(gameObject);
        gameObject.TryGetComponent(out UnitLogic logic);
        if (logic != null)
        {
            Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 1.0f, LayerMask.GetMask("Default"));
            List<Collider> validColliders = new List<Collider>();
            foreach (var col in colliders)
            {
                if (col.gameObject.CompareTag("ComputerShooter") || col.gameObject.CompareTag("ComputerWarrior") ||
                    col.gameObject.CompareTag("ComputerMagician"))
                {
                    validColliders.Add(col);
                }
            }

            if (validColliders.Count >= 5)
            {
                Random random = new Random();
                if (nextLayer < 4)
                {
                    logic.SetMovementDest(GetRandomPosWithLayer(nextLayer).position);
                }
            }
        }
    }

    private void SetInitDest(GameObject gameObject)
    {
        gameObject.TryGetComponent(out UnitLogic logic);
        Random random = new Random();
        int weight = random.Next(10);
        if (weight <= 7)
        {
            if (logic != null)
            {
                logic.SetMovementDest(RallyPos.transform.position);
            }
        }
        else
        {
            if (logic != null)
            {
                logic.SetMovementDest(GetRandomPosWithLayer(1).position);
            }
        }
    }
}