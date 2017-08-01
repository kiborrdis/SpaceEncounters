using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public interface IModelProvider<TModel>
    {
        TModel Model();
    }
}
