using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public abstract class GenericView<TModel, TController> : BaseView, IControllerHolder<TController>, IModelHolder<TModel>
    where TModel : Model
    where TController : BaseController
    {
        public UnityEngine.Object publicModelProvider;

        TModel model;
        TController controller;

        public TModel Model
        {
            get
            {
                if (model == null)
                {

                    ExtractModel();
                }

                return model;
            }
            set
            {
                model = value;
            }
        }

        public virtual void setModel(TModel model)
        {
            this.model = model;
        }

        public virtual TController Controller
        {
            get
            {
                return controller;
            }

            set
            {
                controller = value;
                CastController(value);
            }
        }

        protected virtual void CastModel(Model model)
        {

        }

        protected virtual void CastController(TController view)
        {

        }

        protected TModel GetModelFromProvider(UnityEngine.Object potentialProvider)
        {
            TModel model = null;
            IModelProvider<TModel> modelProvider = null;
            IModelHolder<TModel> modelHolder = null;

            try
            {
                modelProvider = (IModelProvider<TModel>)potentialProvider;
            }
            catch (System.InvalidCastException)
            {
            }

            try
            {
                modelHolder = (IModelHolder<TModel>)potentialProvider;
            }
            catch (System.InvalidCastException)
            {
            }

            if (modelProvider != null)
            {
                model = modelProvider.Model();
            }
            else if (modelHolder != null)
            {
                model = modelHolder.Model;
            }

            return model;
        }

        protected virtual void ExtractModel()
        {
            TModel model = null;
            IModelHolder<TModel> holder = null;
            try
            {
                holder = (IModelHolder<TModel>)Controller;
            } catch(System.InvalidCastException) {
                Debug.Log("GenericView: invalid Controller to IModelHolder cast");
            }

            if (holder != null)
            {
                model = holder.Model;
            }

            if (model == null)
            {
                Model = GetModelFromProvider(publicModelProvider);
            }

            Model = model;
        }

        protected virtual void Start()
        {
            
        }
    }
}
