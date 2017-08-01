using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public abstract class ModelFactory<TModel> : ScriptableObject, IModelProvider<TModel>
    {
        public virtual TModel Model()
        {
            throw new NotImplementedException();
        }
    }
}
