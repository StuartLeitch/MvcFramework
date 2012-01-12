using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Core.EventBroker;
using Application.Core.EventBroker.Interfaces;
using Ninject;

namespace Application.Core.BaseClasses
{
    [HandleErrorWithELMAH]
    public abstract class BaseController : Controller, IUseEventBroker
    {
        protected IEventBroker _eventBroker;

        public IEventBroker EventBroker { get { return this._eventBroker; } }

        public bool HasFlashMessage
        {
            get {
                return
                        this.EventBroker.ValidationFailures.Any(
                                x =>
                                x.FlashLevel == FlashLevelType.Error || x.FlashLevel == FlashLevelType.Info
                                || x.FlashLevel == FlashLevelType.Warning);
            }
        }

        protected string IpAddress { get { return this.Request.ServerVariables["REMOTE_ADDR"]; } }

        public void AddModelStateError(string modelStateKey, string modelStateError)
        {
            if (modelStateKey == null && modelStateError == null)
                throw new ArgumentException("ModelStateKey and ModelStateError cannot be both null");

            // IMPROVE: Use Pascal case splitting to insert space when inferring the required message.

            this.ModelState.AddModelError(modelStateKey ?? string.Empty,
                                          modelStateError ?? string.Format("The {0} field is required", modelStateKey));
        }

        public void AddRule(string errorMessage)
        {
            errorMessage.ThrowIfNull("errorMessage");
            
            this._eventBroker.AddFailure(errorMessage);
        }

        /// <summary>
        ///   Ninject will take care of setting this. If not using BaseTest when testing, you can use BrokerTestFactory to create an instance of BrokerTestVersion
        /// </summary>
        [Inject]
        public void SetEventBrokerFactory(IBrokerFactory brokerFactory)
        {
            this._eventBroker = brokerFactory.GetGeneralBroker();
            this.SetEventBrokerUser();
        }

        protected void AddValidationErrorToModelStateIfAppropriate(IEnumerable<ValidationFailure> validationFailures)
        {
            foreach (var validationFailure in
                    validationFailures.Where(
                            x =>
                            (!string.IsNullOrWhiteSpace(x.ModelStateKey) && !string.IsNullOrWhiteSpace(x.DisplayMessage))
                            || (x.FlashLevel == FlashLevelType.MvcModelState) && !string.IsNullOrWhiteSpace(x.DisplayMessage)))
                this.ModelState.AddModelError(validationFailure.ModelStateKey ?? string.Empty, validationFailure.DisplayMessage);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.ProcessValidationFailuresIfAny();
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        ///   If the flash helpers are displaying, they will display again if the user goes back then forward in the browser. 
        ///   Therefore when there is a flash message, instruct the browser not to cache the page.
        /// </summary>
        /// <param name="filterContext"> </param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.HasFlashMessage)
            {
                this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                this.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                this.Response.Cache.SetNoStore();
                this.Response.AppendHeader("Pragma", "no-cache");
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        ///   Intended to be overridden only by BaseSecureController Call this._eventBroker.SetFunctionToGetWrappedUser(() => FunctionToGetUserGoesHere); 
        ///   in BaseSecureController to give access to user.
        /// </summary>
        protected virtual void SetEventBrokerUser() { }

        private void FlashExceptionChain(Exception ex, string message = null)
        {
            if (!ex.Message.Contains("inner exception"))
                this.FlashError(!string.IsNullOrWhiteSpace(message) ? string.Format("{0} {1}", message, ex.Message) : ex.Message);

            if (ex.InnerException != null)
                this.FlashExceptionChain(ex.InnerException);
        }

        private void ProcessFailure(ValidationFailure failure)
        {
            if (failure.Exception != null)
            {
                this.FlashExceptionChain(failure.Exception, failure.DisplayMessage);
                return;
            }

            // Don't display message in flash if the key is set
            if (failure.HasMessage && string.IsNullOrWhiteSpace(failure.ModelStateKey))
            {
                switch (failure.FlashLevel)
                {
                    case FlashLevelType.Error:
                        this.FlashError(failure.DisplayMessage);
                        break;
                    case FlashLevelType.Warning:
                        this.FlashWarning(failure.DisplayMessage);
                        break;
                    case FlashLevelType.Info:
                        this.FlashInfo(failure.DisplayMessage);
                        break;
                }
            }
        }

        private void ProcessValidationFailuresIfAny()
        {
            this.AddValidationErrorToModelStateIfAppropriate(this._eventBroker.ValidationFailures);

            foreach (var failure in this.EventBroker.ValidationFailures)
                this.ProcessFailure(failure);
        }
    }
}