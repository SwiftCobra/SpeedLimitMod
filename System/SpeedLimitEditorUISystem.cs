using System;
using System.Collections.Generic;
using System.Linq;
using Colossal.Entities;
using Colossal.UI.Binding;
using Game.Net;
using Game.SceneFlow;
using Game.Settings;
using Game.Tools;
using Game.UI;
using Game.UI.InGame;
using Unity.Entities;
using UnityEngine;
using CarLane = Game.Net.CarLane;

namespace SpeedLimitEditor.System;

public class SpeedLimitEditorUISystem : UISystemBase
{
    private readonly string kGroup = "speed_limit_editor";

    private ToolSystem toolSystem;
    private BufferLookup<AggregateElement> aggregateElementLookup;

    private Entity selectedEntity;
    private bool changingSpeed;
    private float averageSpeed;
    private NameSystem nameSystem;
    private string roadName = "";
    private GetterValueBinding<string> unitSystemBinding;
    private InterfaceSettings.UnitSystem unitSystem;


    protected override void OnCreate()
    {
        base.OnCreate();
        toolSystem = World.GetExistingSystemManaged<ToolSystem>();
        nameSystem = World.GetExistingSystemManaged<NameSystem>();
        World.GetOrCreateSystemManaged<SelectedInfoUISystem>();
        aggregateElementLookup = GetBufferLookup<AggregateElement>();
        selectedEntity = Entity.Null;
        unitSystem = GameManager.instance.settings.userInterface.unitSystem;
        
        //Bindings
        AddUpdateBinding(new GetterValueBinding<float>(kGroup, "speed", 
            () => GameManager.instance.settings.userInterface.unitSystem == InterfaceSettings.UnitSystem.Freedom ? Mathf.Round(averageSpeed * 0.62137119f) :  Mathf.Round(averageSpeed)));
        AddUpdateBinding(unitSystemBinding = new GetterValueBinding<string>(kGroup, "unitSystem", () => GameManager.instance.settings.userInterface.unitSystem == InterfaceSettings.UnitSystem.Metric ? "km/h" : "mph"));
        AddUpdateBinding(new GetterValueBinding<string>(kGroup, "name", () => roadName));

        AddBinding(new TriggerBinding<float>(kGroup, "set_speed_limit", HandleSpeedLimitChange));
        
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (changingSpeed)
            return;

        if (GameManager.instance.settings.userInterface.unitSystem != unitSystem)
        {
            unitSystemBinding.Update();
        }

        if (toolSystem.selected == Entity.Null && toolSystem.selected != selectedEntity)
        {
            selectedEntity = Entity.Null;
            averageSpeed = 0f;
            roadName = "";
            return;
        }

        if (toolSystem.selected.Index != selectedEntity.Index)
        {
            roadName = nameSystem.GetRenderedLabelName(toolSystem.selected);
            averageSpeed = 0f;

            if (aggregateElementLookup.TryGetBuffer(toolSystem.selected, out var aggregateElements))
            {
                foreach (var aggregateElement in aggregateElements)
                {
                    if (!EntityManager.TryGetBuffer(aggregateElement.m_Edge, false,
                            out DynamicBuffer<SubLane> subLanes))
                        continue;

                    var speeds = new List<float>();
                    for (int j = 0; j < subLanes.Length; j++)
                    {
                        if (!EntityManager.TryGetComponent(subLanes[j].m_SubLane, out CarLane carLane))
                            continue;

                        speeds.Add(carLane.m_SpeedLimit);
                    }
                    
                    averageSpeed = Mathf.Round(speeds.Average());
                    selectedEntity = toolSystem.selected;
                    break;
                }
            }

            return;
        } 

        if (toolSystem.selected.Index == selectedEntity.Index)
        {
            //do nothing
        }
        
    }

    private void HandleSpeedLimitChange(float suggestedSpeed)
    {
        changingSpeed = true;

        float newSpeed;
        if (GameManager.instance.settings.userInterface.unitSystem == InterfaceSettings.UnitSystem.Freedom)
        {
            newSpeed = suggestedSpeed * 1.609344f;
        }
        else
        {
            newSpeed = suggestedSpeed;

        }

        try
        {
            var aggregateElements = aggregateElementLookup[selectedEntity];
            for (int i = 0; i < aggregateElements.Length; i++)
            {
                if (EntityManager.TryGetBuffer(aggregateElements[i].m_Edge, false,
                        out DynamicBuffer<SubLane> sublanes))
                {
                    for (int j = 0; j < sublanes.Length; j++)
                    {
                        if (!EntityManager.TryGetComponent(sublanes[j].m_SubLane, out CarLane carLane))
                            continue;
                        carLane.m_DefaultSpeedLimit = newSpeed;
                        carLane.m_SpeedLimit = newSpeed;
                        EntityManager.SetComponentData(sublanes[j].m_SubLane, carLane);
                    }
                }
            }

            averageSpeed = newSpeed;
            UnityEngine.Debug.Log($"Done Changing Speed");
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
            throw;
        }

        changingSpeed = false;
    }
}