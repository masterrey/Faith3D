using System;
using AnythingWorld.Utilities.Data;

namespace AnythingWorld.Networking
{
    using Utilities;

    public class JsonProcessor
    {
        /// <summary>
        /// Calls methods to do secondary processing on json data before further creation steps.
        /// </summary>
        /// <param name="data"></param>
        public static void ProcessData(ModelData data)
        {
            data.Debug("Processing data");
            SetAnimationPipeline(data);
            SetBehaviourType(data);
            SetModelLoadingPipeline(data);
            AddModelInspector(data);
            data.actions.loadModelDelegate?.Invoke(data);
        }

        /// <summary>
        /// Decide correct animation pipeline from user request and/or json data.
        /// Set animation pipeline enum based on outcome.
        /// </summary>
        /// <param name="data">Model data for request.</param>
        public static void SetAnimationPipeline(ModelData data)
        {
            //UnityEngine.Debug.Log($"Type: {data.json.type}, {data.json.behaviour}");
            //If requested to be static or type is static use static mesh pipeline override
            if (data?.parameters?.animateModel == false || data.json.behaviour == "static")
            {
                data.animationPipeline = AnimationPipeline.Static;
            }
            else
            {
                data.animationPipeline = ParseAnimationPipeline(data.json);
            }
        }

        public static void SetBehaviourType(ModelData data)
        {
            data.defaultBehaviourType = data.parameters.animateModel ? 
                ParseBehaviourType(data.json, data.parameters.useNavMesh) : DefaultBehaviourType.Static;
        }
        
        public static DefaultBehaviourType ParseBehaviourType(ModelJson json, bool useNavMesh)
        {
            var animationDictionary = json?.model?.rig?.animations;
            var behaviourType = DefaultBehaviourType.Static;
            if (animationDictionary != null && animationDictionary.Count > 0)
            {
                behaviourType = useNavMesh ? 
                    DefaultBehaviourType.NavMeshWalkingAnimal : DefaultBehaviourType.WalkingAnimal;
            }
            if (json.behaviour =="fly")
            {
                if (json.type.Contains("vehicle"))
                {
                    behaviourType = DefaultBehaviourType.FlyingVehicle;
                }
                else
                {
                    behaviourType = DefaultBehaviourType.FlyingAnimal;
                }
            }
            else if (json.behaviour.Contains("swim"))
            {
                if (json.type.Contains("vehicle"))
                {
                    //behaviourType = DefaultBehaviourType.FlyingVehicle;
                }
                else
                {
                    behaviourType = DefaultBehaviourType.SwimmingAnimal;
                }
            }
            else if (json.behaviour == "drive")
            {
                behaviourType =  DefaultBehaviourType.WheeledVehicle;
            }
            else if (json.type == "uniform")
            {
                if (json.behaviour == "static")
                {
                    behaviourType = DefaultBehaviourType.Static;
                }
            }
            return behaviourType;
        }
        
        public static AnimationPipeline ParseAnimationPipeline(ModelJson json)
        {
            var animationDictionary = json?.model?.rig?.animations;
            //If walk rig url is present use rigged pipeline
            if (animationDictionary != null && animationDictionary.Count > 0)
            {
                return AnimationPipeline.Rigged;
            }
            
            //If behaviour is type drive set animation to drive
            //(lots of different car types, switch to consistent type based)
            if (json.behaviour == "drive")
            {
                return AnimationPipeline.WheeledVehicle;
            }

            return json.type switch
            {
                "vehicle_propeller" => AnimationPipeline.PropellorVehicle,
                //If type is uniform use shader animation pipeline.
                "uniform" when json.behaviour == "static" => AnimationPipeline.Static,
                "uniform" => AnimationPipeline.Shader,
                _ => AnimationPipeline.Static
            };
        }
        /// <summary>
        /// Check if the model was marqued as animated by the API server
        /// </summary>
        /// <param name="json">json to check</param>
        /// <returns></returns>
        public static bool CheckIfTheModelIsAnimated(ModelJson json)
        {
            bool maybeAnimated = (bool)(json?.animated);

            return maybeAnimated;
        }

        /// <summary>
        /// Set model loading pipeline, currently hard linked to animation type but we could offer different options in future.
        /// </summary>
        /// <param name="data">Model data for request.</param>
        public static void SetModelLoadingPipeline(ModelData data)
        {
            switch (data.animationPipeline)
            {
                case AnimationPipeline.Static:
                    data.modelLoadingPipeline = ModelLoadingPipeline.OBJ_Static;
                    break;
                
                case AnimationPipeline.Rigged:
                    data.modelLoadingPipeline = ModelLoadingPipeline.RiggedGLB;
                    break;
                
                case AnimationPipeline.WheeledVehicle:
                case AnimationPipeline.PropellorVehicle:
                    data.modelLoadingPipeline = ModelLoadingPipeline.OBJ_Part_Based;
                    break;
                
                case AnimationPipeline.Shader:
                    data.modelLoadingPipeline = ModelLoadingPipeline.OBJ_Static;
                    break;
                
                case AnimationPipeline.Unset:
                    data.modelLoadingPipeline = ModelLoadingPipeline.Unset;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(data.animationPipeline),
                        $"Not expected animation pipeline value: {data.animationPipeline}");
            }
        }
        /// <summary>
        /// Add inspector component to model game object to allow user to edit model parameters in editor.
        /// </summary>
        private static void AddModelInspector(ModelData data)
        {
            ModelDataInspector inspector = data.model.AddComponent<ModelDataInspector>();
            inspector.Populate(data);
            bool hasSpeed = false;
            foreach (LabelledFloat lf in inspector.movement)
            {
                if (lf.label.ToLower() == "speed")
                {
                    hasSpeed = true;
                }
            }
            // set a default speed if none is found in the model data (messes animations otherwise)
            if (!hasSpeed)
            {
                inspector.movement.Add(new LabelledFloat("Speed", 42));
            }
            data.model.name = data.json.name;
        }
    }
}
