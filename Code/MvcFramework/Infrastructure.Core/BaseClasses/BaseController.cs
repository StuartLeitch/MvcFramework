using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Core.EventBroker;
using Infrastructure.Core.EventBroker.Interfaces;
using Infrastructure.Core.Validation;
using Ninject;

namespace Infrastructure.Core.BaseClasses
{
    [HandleErrorWithELMAH]
    public abstract class BaseController : Controller, IUseEventBroker
    {
        protected IEventBroker _eventBroker;

        public IEventBroker EventBroker
        {
            get
            {
                return this._eventBroker;
            }
        }

        /// <summary>
        ///   Will be true if a ValidationFailure event has taken place or if a FlashMessage
        ///   has been explicitly raised via FlashHelper
        /// </summary>
        public bool HasFlashMessage
        {
            get
            {
                // Only need to check for the first of each type.
                return this.TempData[FlashHelpers.Error + "1"] != null || this.TempData[FlashHelpers.Warning + "1"] != null ||
                       this.TempData[FlashHelpers.Info + "1"] != null;
            }
        }

        protected string IpAddress
        {
            get
            {
                return this.Request.ServerVariables["REMOTE_ADDR"];
            }
        }

        public void AddModelStateError(string modelStateKey, string modelStateError)
        {
            if (modelStateKey == null && modelStateError == null)
                throw new ArgumentException("ModelStateKey and ModelStateError cannot be both null");

            // IMPROVE: Use Pascal case splitting to insert space when inferring the required message.

            this.ModelState.AddModelError(
                modelStateKey ?? string.Empty, modelStateError ?? string.Format("The {0} field is required", modelStateKey));
        }

        public void AddRule(string errorMessage)
        {
            this._eventBroker.AddFailure(errorMessage);
        }

        /// <summary>
        ///   Ninject will take care of setting this.  
        ///   If not using BaseTest when testing, you can use BrokerTestFactory to create 
        ///   an instance of BrokerTestVersion
        /// </summary>
        [Inject]
        public void SetEventBrokerFactory(IBrokerFactory brokerFactory)
        {
            this._eventBroker = brokerFactory.GetGeneralBroker();

            // Subscribe to Validation issues raised by Repositories and Services
            this._eventBroker.ValidationFailedEvent += this.ValidationFailedEvent;

            this.SetEventBrokerUser();
        }

        protected void AddValidationErrorToModelState(IEnumerable<ValidationFailure> validationFailures)
        {
            foreach (
                var validationFailure in
                    validationFailures.Where(
                        x =>
                        (!string.IsNullOrWhiteSpace(x.ModelStateKey) && !string.IsNullOrWhiteSpace(x.Message)) ||
                        (x.DisplayUiWithoutKey) && !string.IsNullOrWhiteSpace(x.Message)))
            {
                this.ModelState.AddModelError(validationFailure.ModelStateKey ?? string.Empty, validationFailure.Message);
            }
        }

        /// <summary>
        ///   If the flash helpers are displaying, they will display again if the user goes back then forward in the browser.
        ///   Therefore when there is a flash message, instruct the browser not to cache the page.
        /// </summary>
        /// <param name = "filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.HasFlashMessage)
            {
                this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                this.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                this.Response.Cache.SetNoStore();
                this.Response.AppendHeader("Pragma", "no-cache");
            }
        }

        /// <summary>
        ///   Intended to be overridden only by BaseSecureController
        ///   Call this._eventBroker.SetFunctionToGetWrappedUser(() => FunctionToGetUserGoesHere); 
        ///   in BaseSecureController to give access to user.
        /// </summary>
        protected virtual void SetEventBrokerUser() {}

        /// <summary>
        ///   Is subscribed to the EventBroker validation failed event. 
        ///   Pushes failures to the View.
        /// </summary>
        protected void ValidationFailedEvent(object sender, ValidationFailedEventArgs eventArgs)
        {
            this.AddValidationErrorToModelState(this._eventBroker.GetFailures());

            foreach (var failure in this.EventBroker.GetFailures().Where(x => x.Exception != null))
            {
                this.WriteExceptionToModelState(failure.Exception, 1);
            }

            // Don't display message in flash if the key is set
            if (eventArgs.HasMessage && string.IsNullOrWhiteSpace(eventArgs.Key))
            {
                switch (eventArgs.FlashLevel)
                {
                    case ValidationFailedEventArgs.FlashLevelType.Error:
                        this.FlashError(eventArgs.DisplayMessage);
                        break;
                    case ValidationFailedEventArgs.FlashLevelType.Warning:
                        this.FlashWarning(eventArgs.DisplayMessage);
                        break;
                    case ValidationFailedEventArgs.FlashLevelType.Info:
                        this.FlashInfo(eventArgs.DisplayMessage);
                        break;
                }
            }
            else
            {
                this.FlashError("Validation Error");

                // IMPROVE: This is likely problematic.
                // it works for partials where the validation does not match, but will display these extra messages
                // even when the model state matches.
                foreach (var failure in this.EventBroker.GetFailures().Where(x => !string.IsNullOrWhiteSpace(x.Message)))
                {
                    this.FlashError(failure.Message);
                }
            }
        }

        private void WriteExceptionToModelState(Exception ex, int indentDepth)
        {
            if (!ex.Message.Contains("inner exception"))
                this.AddModelStateError("", ex.Message);

            if (ex.InnerException != null)
                this.WriteExceptionToModelState(ex.InnerException, indentDepth + 1);
        }
    }
}