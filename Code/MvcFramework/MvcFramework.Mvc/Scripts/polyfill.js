
Modernizr.load([
{
    test: Modernizr.inputtypes && Modernizr.inputtypes.date,
    nope: '/Scripts/jquery-ui-1.8.16.min.js', 
    complete: function () {
                $(function () {
                    $("input.date-picker").datepicker();
                });
            }
}
]);












