using AnythingWorld.Utilities.Data;
using System;
using UnityEngine;

namespace AnythingWorld
{
    public static class AnythingMaker
    {
        /// <summary>
        /// Request object that's the closest to the search term.
        /// </summary>
        /// <param name="name">String term to find close match to.</param>
        /// <param name="parameters">Array of RequestParameterOption objects holding parameters to customise make process.</param>
        /// <returns></returns>
        public static GameObject Make(string name, params RequestParameterOption[] parameters)
        {
            if (name == "dog") name = "dog#0001";
            //Fetches data from user input and clears request static variables ready for next request.
            var requestParams = RequestParameter.Fetch();
            return Core.AnythingFactory.RequestModel(name, requestParams);
        }
      /// <summary>
      /// Request object exactly matching the JSON provided.
      /// </summary>
      /// <param name="json"></param>
      /// <param name="parameters"></param>
      /// <returns></returns>
        public static GameObject Make(ModelJson json, params RequestParameterOption[] parameters)
        {
            if(AnythingSettings.DebugEnabled)
            {
                Debug.Log("Making model by json");
            }
            //Fetches data from user input and clears request static variables ready for next request.
            var requestParams = RequestParameter.Fetch();

            return Core.AnythingFactory.RequestProcessedModel(json, requestParams);

        }
        /// <summary>
        /// Request object exactly matching the ID provided.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static GameObject MakeById(string id, params RequestParameterOption[] parameters)
        {
            if(AnythingSettings.DebugEnabled)
            {
                Debug.Log("Making model by id");
            }
            
            //Fetches data from user input and clears request static variables ready for next request.
            var requestParams = RequestParameter.Fetch();

            return Core.AnythingFactory.RequestModelById(id, requestParams);
        }
        /// <summary>
        /// Request object using the JSON provided.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="requestParameterOptions"></param>
        /// <returns></returns>
        public static GameObject MakeByJson(ModelJson json, RequestParameterOption[] requestParameterOptions)
        {
            if(json == null)
            {
                Debug.LogError("ModelJson is null");
                return null;
            }
            
            if (AnythingSettings.DebugEnabled)
            {
                Debug.Log("Making model by json for featured");
            }
           
            //Fetches data from user input and clears request static variables ready for next request.
            RequestParamObject requestParams = RequestParameter.Fetch();
            return Core.AnythingFactory.RequestModel(json, requestParams);
        }
    }
}
