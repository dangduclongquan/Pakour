using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] Mountable Pivot;
    [SerializeField] GameObject FirstPersonCamera;
    [SerializeField] GameObject ThirdPersonCamera;
    [SerializeField] CameraZoom CameraZoom;
    
    [Range(-20, 0)]
    [SerializeField] float MaxZoom = 0;
    [Range(-20, 0)]
    [SerializeField] float MinZoom = -5;

    public string ViewMode {get; private set;}

    void Awake()
    {
        CameraZoom.minZ = MinZoom;
        CameraZoom.maxZ = MaxZoom;

        if (MaxZoom < MinZoom)
        {
            MaxZoom = MinZoom;
        }
        ViewMode = "First person";
    }

    Survivor attached;
    public void Attach(Survivor survivor)
    {
        if (attached != null)
            attached.SurvivorKilledEvent -= OnAttachedSurvivorDied;
        
        Pivot.Mount(survivor.Head);
        survivor.SurvivorKilledEvent += OnAttachedSurvivorDied;
        attached = survivor;
        ChangeViewMode(ViewMode);
    }

    public void ChangeViewMode(string viewMode)
    {
        if (viewMode == "First person")
        {
            attached.DisplayFirstPersonModel();
            FirstPersonCamera.SetActive(true);
            ThirdPersonCamera.SetActive(false);
            // TODO
        }
        else
        {
            attached.DisplayThirdPersonModel();
            ThirdPersonCamera.SetActive(true);
            FirstPersonCamera.SetActive(false);
            // TODO
        }
        ViewMode = viewMode;
    }

    public void OnViewModeToggle(InputValue inputvalue)
    {
        if (ViewMode == "Third person")
            ViewMode = "First person";
        else
            ViewMode = "Third person";
        ChangeViewMode(ViewMode);
    }

    public void OnAttachedSurvivorDied()
    {
        // TODO
    }
}
