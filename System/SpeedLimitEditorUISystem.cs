using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Colossal.Entities;
using Colossal.UI.Binding;
using Game;
using Game.Common;
using Game.Net;
using Game.Simulation;
using Game.Tools;
using Game.UI;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using CarLane = Game.Net.CarLane;

namespace SpeedLimitEditor.System;

public class SpeedLimitEditorSystem : GameSystemBase
{
    private ToolSystem toolSystem;
    private BufferLookup<SubLane> sublaneLookup;
    private BufferLookup<AggregateElement> aggregateElementLookup;

    protected override void OnCreate()
    {
        base.OnCreate();
        toolSystem = World.GetExistingSystemManaged<ToolSystem>();
        sublaneLookup = GetBufferLookup<SubLane>(false);
        aggregateElementLookup = GetBufferLookup<AggregateElement>(false);
        CreateKeyBinding();
    }

    protected override void OnUpdate()
    {
        
    }

    private void CreateKeyBinding()
    {
        var inputAction = new InputAction("ToggleWhiteness");
        inputAction.AddCompositeBinding("ButtonWithOneModifier")
            .With("Modifier", "<Keyboard>/ctrl")
            .With("Button", "<Keyboard>/s");
        inputAction.performed += OnToggleWhiteness;
        inputAction.Enable();
    }
    private void OnToggleWhiteness(InputAction.CallbackContext obj)
    {
        ToggleWhiteness();
    }

    private void ToggleWhiteness()
    {
        try
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var aggregateElements = aggregateElementLookup[toolSystem.selected];
            for (int i = 0; i < aggregateElements.Length; i++)
            {
                var aggregateElementShouldUpdate = false;

                if (EntityManager.TryGetBuffer(aggregateElements[i].m_Edge, false,
                        out DynamicBuffer<SubLane> sublanes))
                {

                    for (int j = 0; j < sublanes.Length; j++)
                    {
                        if (!EntityManager.TryGetComponent(sublanes[j].m_SubLane, out CarLane carLane))
                            continue;

                        //carLane.m_Flags &= ~CarLaneFlags.UTurnLeft;
                        //carLane.m_Flags &= ~CarLaneFlags.UTurnRight;
                        //carLane.m_Flags &= ~CarLaneFlags.ParkingLeft;
                        //carLane.m_Flags &= ~CarLaneFlags.ParkingRight;
                        carLane.m_DefaultSpeedLimit = 1f;
                        carLane.m_SpeedLimit = 1f;
                        aggregateElementShouldUpdate = true;
                        EntityManager.SetComponentData(sublanes[j].m_SubLane, carLane);
                        //EntityManager.AddComponent<Updated>(sublanes[j].m_SubLane);
                    }
                }
                //if (aggregateElementShouldUpdate)
                //{
                //    EntityManager.SetComponentData(aggregateElements[i].m_Edge, new Updated());
                //}
            }
            stopWatch.Stop();

            UnityEngine.Debug.Log($"Elapsed Time {stopWatch.ElapsedMilliseconds}ms");
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
            throw;
        }
        

        //var entities = entityQuery.ToEntityArray(Allocator.TempJob);

        //var selected = entities.FirstOrDefault(e => e.Index == toolSystem.selected.Index);

        //UnityEngine.Debug.Log($"Looking up entities {entities}");
        //if (sublaneLookup.HasBuffer(toolSystem.selected))
        //{
        //    UnityEngine.Debug.Log($"Inside Sublane Entity");
        //    DynamicBuffer<SubLane> buffer = sublaneLookup[toolSystem.selected];
        //    foreach (SubLane subLane in buffer)
        //    {
        //        UnityEngine.Debug.Log($"for each sublane: {subLane.m_SubLane.Index}");
        //        if (carLaneLookup.HasComponent(subLane.m_SubLane))
        //        {
        //            UnityEngine.Debug.Log($"car lane lookup");
        //            var carLane = carLaneLookup[subLane.m_SubLane];

        //            UnityEngine.Debug.Log($"car lane: {carLane.m_DefaultSpeedLimit} | {carLane.m_SpeedLimit}");
        //            //carLane.m_DefaultSpeedLimit = 100;
        //            //carLane.m_SpeedLimit = 100;
        //        }
        //    }
        //}

        

        //    UnityEngine.Debug.Log($"Selected Index: {toolSystem.selected.Index}");
        //    var creationDefinition = EntityManager.GetComponentObject<NetLaneInfo>(toolSystem.selected);
        //    //var componentTypes = ListEntityComponents(creationDefinition.m_Lane.components);
        //    UnityEngine.Debug.Log($"CreationDefinition Index: {creationDefinition}");

        //    //foreach (var componentType in componentTypes)
        //    //{
        //    //    UnityEngine.Debug.Log($"ComponentType: {componentType}");
        //    //    UnityEngine.Debug.Log($"Type Index: {componentType.TypeIndex.ToString()}");
        //    //    UnityEngine.Debug.Log($"Managed Type: {componentType.GetManagedType().Name}");
        //    //    UnityEngine.Debug.Log($"CO JSON: {componentType.ToJSONString()}");
        //    //    UnityEngine.Debug.Log($"JSON: {JsonConvert.SerializeObject(componentType)}");
        //    //}
        //}

        //if (EntityManager.HasComponent<NetLaneData>(toolSystem.selected))
        //{
        //    UnityEngine.Debug.Log($"Has Net lane Data");
        //    UnityEngine.Debug.Log($"Selected Index: {toolSystem.selected.Index}");
        //    var creationDefinition = EntityManager.GetComponentObject<NetLaneInfo>(toolSystem.selected);
        //    //var componentTypes = ListEntityComponents(creationDefinition.m_Lane.components);
        //    UnityEngine.Debug.Log($"CreationDefinition Index: {creationDefinition}");

        //    //foreach (var componentType in componentTypes)
        //    //{
        //    //    UnityEngine.Debug.Log($"ComponentType: {componentType}");
        //    //    UnityEngine.Debug.Log($"Type Index: {componentType.TypeIndex.ToString()}");
        //    //    UnityEngine.Debug.Log($"Managed Type: {componentType.GetManagedType().Name}");
        //    //    UnityEngine.Debug.Log($"CO JSON: {componentType.ToJSONString()}");
        //    //    UnityEngine.Debug.Log($"JSON: {JsonConvert.SerializeObject(componentType)}");
        //    //}
        //}

        //if (EntityManager.HasComponent<NetLanePrefab>(toolSystem.selected))
        //{
        //    UnityEngine.Debug.Log($"Has NetLanePrefab");
        //    UnityEngine.Debug.Log($"Selected Index: {toolSystem.selected.Index}");
        //    var creationDefinition = EntityManager.GetComponentObject<NetLaneInfo>(toolSystem.selected);
        //    //var componentTypes = ListEntityComponents(creationDefinition.m_Lane.components);
        //    UnityEngine.Debug.Log($"CreationDefinition Index: {creationDefinition}");

        //    //foreach (var componentType in componentTypes)
        //    //{
        //    //    UnityEngine.Debug.Log($"ComponentType: {componentType}");
        //    //    UnityEngine.Debug.Log($"Type Index: {componentType.TypeIndex.ToString()}");
        //    //    UnityEngine.Debug.Log($"Managed Type: {componentType.GetManagedType().Name}");
        //    //    UnityEngine.Debug.Log($"CO JSON: {componentType.ToJSONString()}");
        //    //    UnityEngine.Debug.Log($"JSON: {JsonConvert.SerializeObject(componentType)}");
        //    //}
        //}

        //if (EntityManager.HasComponent<NetCompositionLane>(toolSystem.selected))
        //{
        //    UnityEngine.Debug.Log($"Has Net Composition lane");
        //    UnityEngine.Debug.Log($"Selected Index: {toolSystem.selected.Index}");
        //    var creationDefinition = EntityManager.GetComponentObject<NetCompositionLane>(toolSystem.selected);
        //    var componentTypes = ListEntityComponents(creationDefinition.m_Lane);
        //    UnityEngine.Debug.Log($"CreationDefinition Index: {creationDefinition.m_Lane.Index}");

        //    foreach (var componentType in componentTypes)
        //    {
        //        EntityManager.GetAssignableComponentTypes()
        //        UnityEngine.Debug.Log($"ComponentType: {componentType}");
        //        UnityEngine.Debug.Log($"Type Index: {componentType.TypeIndex.ToString()}");
        //        UnityEngine.Debug.Log($"Managed Type: {componentType.GetManagedType().Name}");
        //        UnityEngine.Debug.Log($"CO JSON: {componentType.ToJSONString()}");
        //        UnityEngine.Debug.Log($"JSON: {JsonConvert.SerializeObject(componentType)}");
        //    }
        //}


    }

    public List<ComponentType> ListEntityComponents(Entity entity)
    {
        var componentTypes = new List<ComponentType>();

        if (!EntityManager.Exists(entity))
            throw new ArgumentException("Entity Does Not Exist");

        using (NativeArray<ComponentType> types = EntityManager.GetComponentTypes(entity, Allocator.Temp))
        {
            foreach (var type in types)
            {
                componentTypes.Add(type);
            }
        }

        return componentTypes;
    }
}

public class SpeedLimitEditorUISystem : UISystemBase
{
    //private ImmutableDictionary<string, MeterSetting> meters;

    private string kGroup = "speed_limit_editor";
    protected override void OnCreate()
    {
        base.OnCreate();

        this.AddBinding(new TriggerBinding<string, bool>(kGroup, "toggle_speed_limit_editor", ToggleSpeedLimitEditor));
    }

    private void ToggleSpeedLimitEditor(string key, bool newValue)
    {
        this.World.GetOrCreateSystemManaged<ZoneSpawnSystem>().debugFastSpawn = newValue;
    }
}