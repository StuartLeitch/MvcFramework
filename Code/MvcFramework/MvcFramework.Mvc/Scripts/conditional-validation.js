/// <reference path="jquery-1.7-vsdoc.js" />
/// <reference path="jquery.validate.unobtrusive.js" />

// Adapted from http://blogs.msdn.com/b/simonince/archive/2011/02/04/conditional-validation-in-asp-net-mvc-3.aspx

// Required If Validator for RequiredIfAttribute (property is required if the dependentProperty equals any of the target values).
$.validator.addMethod('requiredif',
    function(value, element, parameters) {
        var id = '#' + parameters['dependentproperty'];

        // get the target value (as a string,
        // as that's what actual value will be)
        var targetvalue = parameters['targetvalue'];
        targetvalue = (targetvalue == null ? '' : targetvalue).toString();

        var targetvaluearray = targetvalue.split('|');

        for (var i = 0; i < targetvaluearray.length; i++) {

            // get the actual value of the target control
            // note - this probably needs to cater for more
            // control types, e.g. radios
            var control = $(id);
            var controltype = control.attr('type');
            var actualvalue = controltype === 'checkbox' ?
                control.attr('checked') ? "true" : "false" :
                control.val();

            // if the condition is true, reuse the existing
            // required field validator functionality
            if (targetvaluearray[i] === actualvalue) {
                return $.validator.methods.required.call(this, value, element, parameters);
            }
        }

        return true;
    }
);


$.validator.unobtrusive.adapters.add(
    'requiredif',
    ['dependentproperty', 'targetvalue'],
    function(options) {
        options.rules['requiredif'] = {
            dependentproperty: options.params['dependentproperty'],
            targetvalue: options.params['targetvalue']
        };
        options.messages['requiredif'] = options.message;
    });


// Required If Not for RequiredIfAttribute (property is required if the dependentProperty does not equal the targetValue).

$.validator.addMethod('requiredifnot',
    function(value, element, parameters) {
        var id = '#' + parameters['dependentproperty'];

        // get the target value (as a string, 
        // as that's what actual value will be)
        var targetvalue = parameters['targetvalue'];
        targetvalue =
            (targetvalue == null ? '' : targetvalue).toString();

        // get the actual value of the target control
        // note - this probably needs to cater for more 
        // control types, e.g. radios
        var control = $(id);
        var controltype = control.attr('type');
        var actualvalue = controltype === 'checkbox' ?
            control.attr('checked').toString() :
            control.val();

        // if the condition is true, reuse the existing 
        // required field validator functionality
        if (targetvalue !== actualvalue)
            return $.validator.methods.required.call(
                this, value, element, parameters);

        return true;
    }
);

$.validator.unobtrusive.adapters.add(
    'requiredifnot',
    ['dependentproperty', 'targetvalue'],
    function(options) {
        options.rules['requiredifnot'] = {
            dependentproperty: options.params['dependentproperty'],
            targetvalue: options.params['targetvalue']
        };
        options.messages['requiredifnot'] = options.message;
    });


(function($) {
    jQuery.validator.addMethod("notequalto", function(value, element, params) {
        if (this.optional(element)) return true;

        var props = params.split(',');

        for (var i in props) {
            if ($('#' + props[i]).val() == value) return false;
        }

        return true;
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("notequalto", "otherproperties");

}(jQuery));


(function($) {
    jQuery.validator.addMethod("notbothpopulated", function(value, element, params) {
        if (this.optional(element)) return true;

        if (value !== undefined) {
            if ($('#' + params).val() !== undefined && $('#' + params).val() !== "") {
                return false;
            }
        }
        return true;
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("notbothpopulated", "otherproperty");

}(jQuery));

(function($) {
    jQuery.validator.addMethod("requireifpopulated", function(value, element, params) {
        if ($('#' + params).val() !== undefined && $('#' + params).val() !== "" && (value == undefined || value === "")) {
            return false;
        }
        return true;
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("requireifpopulated", "otherproperty");

}(jQuery));

(function($) {
    jQuery.validator.addMethod("requireifnotpopulated", function(value, element, params) {
        if ($('#' + params).val() === undefined || $('#' + params).val() === "") {
            return false;
        }
        return true;
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("requireifnotpopulated", "otherproperty");

}(jQuery));