using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitsManagerLogic : UnitsManagerLogic
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        warriors = GameObject.FindGameObjectsWithTag("PlayerWarrior");
        magicians = GameObject.FindGameObjectsWithTag("PlayerMagician");
        shooters = GameObject.FindGameObjectsWithTag("PlayerShooter");
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            warriorsSelected = !warriorsSelected;
            if (!warriorsSelected)
            {
                foreach(var obj in warriors)
                {
                    Interactive interactive = obj.GetComponent<Interactive>();
                    if (interactive.GetSelected())
                    {
                        interactive.selected = false;
                    }
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            magiciansSelected = !magiciansSelected;
            if (!magiciansSelected)
            {
                foreach(var obj in magicians)
                {
                    Interactive interactive = obj.GetComponent<Interactive>();
                    if (interactive.GetSelected())
                    {
                        interactive.selected = false;
                    }
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            shootersSelected = !shootersSelected;
            if (!shootersSelected)
            {
                foreach(var obj in shooters)
                {
                    Interactive interactive = obj.GetComponent<Interactive>();
                    if (interactive.GetSelected())
                    {
                        interactive.selected = false;
                    }
                }
            }
        }
        
        if (warriorsSelected)
        {
                foreach (var obj in warriors)
                {
                    Interactive interactive = obj.GetComponent<Interactive>();
                    if (!interactive.GetSelected())
                    {
                        interactive.selected = true;
                    }
                }
        }
        
        if (magiciansSelected)
        {
            foreach (var obj in magicians)
            {
                Interactive interactive = obj.GetComponent<Interactive>();
                if (!interactive.GetSelected())
                {
                    interactive.selected = true;
                }
            }
        }
        
        if (shootersSelected)
        {
            foreach (var obj in shooters)
            {
                Interactive interactive = obj.GetComponent<Interactive>();
                if (!interactive.GetSelected())
                {
                    interactive.selected = true;
                }
            }
        }
        
        if (Input.GetMouseButtonDown(0)) // Left Key
        {
            MoveUnitToMousePosition();
        }
    }

    private void LateUpdate()
    {
        warriors = GameObject.FindGameObjectsWithTag("PlayerWarrior");
        magicians = GameObject.FindGameObjectsWithTag("PlayerMagician");
        shooters = GameObject.FindGameObjectsWithTag("PlayerShooter");
    }

    void MoveUnitToMousePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (warriorsSelected)
            {
                foreach (var obj in warriors)
                {
                    if (obj.GetComponent<UnitLogic>().status == UnitLogic.UnitStatus.Move)
                    {
                        obj.GetComponent<UnitLogic>().SetMovementDest(hit.point);
                    }

                }
            }

            if (magiciansSelected)
            {
                foreach (var obj in magicians)
                {
                    if (obj.GetComponent<UnitLogic>().status == UnitLogic.UnitStatus.Move)
                    {
                        obj.GetComponent<UnitLogic>().SetMovementDest(hit.point);
                    }
                }
            }
            
            if (shootersSelected)
            {
                foreach (var obj in shooters)
                {
                    if (obj.GetComponent<UnitLogic>().status == UnitLogic.UnitStatus.Move)
                    {
                        obj.GetComponent<UnitLogic>().SetMovementDest(hit.point);
                    }
                }
            }
        }
    }
}