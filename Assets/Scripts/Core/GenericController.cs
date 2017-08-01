using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public abstract class GenericController<TModel, TView, TController> : BaseController, IModelHolder<TModel>, IViewHolder<TView>
    where TModel : Model
    where TView : BaseView
    where TController : GenericController<TModel, TView, TController>
    {
        public TView publicView;
        public UnityEngine.Object publicModelProvider;

        protected IModelProvider<TModel> modelProvider;
        TModel model;
        TView view;

        public virtual TModel Model
        {
            get
            {
                if (model == null && publicModelProvider != null)
                {
                    BuildModel();
                }

                return model;
            }
            set
            {
                model = value;
            }
        }

        public virtual TView View
        {
            get
            {
                return view;
            }

            set
            {
                view = value;
            }
        }

        protected virtual void CastModel(Model model)
        {

        }

        protected virtual void CastView(TView view)
        {

        }
        
        private void BuildModel()
        {
            if (publicModelProvider != null)
            {
                Model = getModelFromProvider();
                return;
            }

            Model = null;
        }

        private TModel getModelFromProvider()
        {
            TModel model = null;
            IModelProvider<TModel> modelProvider = null;
            IModelHolder<TModel> modelHolder = null;

            try
            {
                modelProvider = (IModelProvider<TModel>)publicModelProvider;
            }
            catch (System.InvalidCastException)
            {
                Debug.LogWarning("Controller: Object to IModelProvider cast error");
            }
            if (modelProvider == null)
            {
                try
                {
                    modelHolder = (IModelHolder<TModel>)publicModelProvider;
                }
                catch (System.InvalidCastException)
                {
                    Debug.LogWarning("Controller: Object to IModelHolder cast error");
                }
            }

            if (modelProvider != null)
            {
                model = modelProvider.Model();
            } else if (modelHolder != null)
            {
                model = modelHolder.Model;
            }

            return model;
        }

        protected virtual void Start()
        {
            if (model == null)
            {
                BuildModel();
            }

            View = publicView;

            if (View != null)
            {
                try
                {
                    IControllerHolder<TController> holder = (IControllerHolder<TController>)View;
                    
                    holder.Controller = (TController)this;

                    //Debug.Log(this + " -- " + holder + " : " + holder.Controller);
                } catch (InvalidCastException)
                {
                    Debug.LogWarning("Controller: invalid View to ControllerHolder cast");
                }
                
            }

            if (view != null)
            {
                try
                {
                    IModelHolder<TModel> holder = (IModelHolder<TModel>)View;

                    holder.Model = model;
                }
                catch (InvalidCastException)
                {
                    Debug.LogWarning("Controller: invalid View to ModelHolder cast");
                }
            }
        }
    }
}
