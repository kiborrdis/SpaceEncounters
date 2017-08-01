using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class ModelAccessor<TModel> : MonoBehaviour, IModelProvider<TModel>
    {
        public UnityEngine.Object modelProvider;
        private TModel model;

        private void Build()
        {
            try
            {
                ModelFactory<TModel> modelFactory = (ModelFactory<TModel>) modelProvider;

                model = modelFactory.Model();
                return;
            } catch(InvalidCastException e)
            {
                Debug.Log("ModelAccessor: invalid cast Object to ModelFactory");
            }

            model = default(TModel);

            return;
        }

        public TModel Model()
        {
            if (model == null)
            {
                Build();
            }

            return model;
        }
    }
}
