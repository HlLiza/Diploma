var currentTab = 0; // Current tab is set to be the first tab (0)
showTab(currentTab); // Display the crurrent tab

function showTab(n) {
    // This function will display the specified tab of the form...
    var x = document.getElementsByClassName("tab");
    x[n].style.display = "block";
    //... and fix the Previous/Next buttons:
    if (n == 0) {
        document.getElementById("prevBtn").style.display = "none";
    } else {
        document.getElementById("prevBtn").style.display = "inline";
    }
    if (n == (x.length - 1)) {
        document.getElementById("nextBtn").innerHTML = "ОК";
    } else {
        document.getElementById("nextBtn").innerHTML = "Далее";
    }
    //... and run a function that will display the correct step indicator:

}

function nextPrev(n) {
    // This function will figure out which tab to display
    var x = document.getElementsByClassName("tab");
    // Exit the function if any field in the current tab is invalid:
    if (n == 1 && !validateForm()) return false;
    // Hide the current tab:
    x[currentTab].style.display = "none";
    // Increase or decrease the current tab by 1:
    currentTab = currentTab + n;
    // if you have reached the end of the form...
    if (currentTab >= x.length) {
        // ... the form gets submitted:
        document.getElementById("regForm").submit();
        return false;
    }
    // Otherwise, display the correct tab:
    showTab(currentTab);
}

function validateForm() {
    // This function deals with validation of the form fields
    var x, y, i, valid = true;
    x = document.getElementsByClassName("tab");
    y = x[currentTab].getElementsByTagName("input");
    // A loop that checks every input field in the current tab:
    for (i = 0; i < y.length; i++) {
        // If a field is empty...
        if (y[i].value == "" && y[i].name!="Skype") {
            // add an "invalid" class to the field:
            y[i].className += " invalid";
            // and set the current valid status to false
            valid = false;
        }
    }
    // If the valid status is true, mark the step as finished and valid:

    return valid; // return the valid status
}

$(document).ready(function () {
    $(function () {

        $("#datepicker1").datepicker({
            format: " yyyy", // Notice the Extra space at the beginning
            viewMode: "years",
            minViewMode: "years"
        });

        $("#datepicker2").datepicker({
            format: " yyyy", // Notice the Extra space at the beginning
            viewMode: "years",
            minViewMode: "years"
        });
    });
});

$(document).ready(function () {

    $(function () {
        $('#datetimepicker1').datetimepicker({
            viewMode: 'years',
            format: 'YYYY'
        });
    });
});


//$(document).ready(function () {

//    $("#datepicker1").datepicker({
//        format: " yy", // Notice the Extra space at the beginning
//        viewMode: "years"
//        //minViewMode: "years"
//    });

    //$("#datepicker1").datepicker({
    //    changeMonth: false,
    //    changeYear: true,
    //    yearRange: "1950:2018",
    //    onClose: function (dateText, inst) {
    //        var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
    //        $(this).datepicker('setDate', new Date(year));
    //    }
    //});


    //$("#datepicker2").datepicker({
    //    changeMonth: false,
    //    changeYear: true,
    //    yearRange: "1960:2030",
    //    onClose: function (dataText, inst) {
    //        var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
    //        $(this).datepicker('setDate', new Date(year));
    //    }
    //});
    //$("#datepicker2").datepicker('setDate', new Number(year));




    //$(function () {
    //    $('.datepicker1').datepicker({
    //        changeYear: true,
    //        showButtonPanel: true,
    //        dateFormat: 'yy',
    //        onClose: function (dateText, inst) {
    //            var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
    //            $(this).datepicker('setDate', new Date(year, 1));
    //        }
    //    });
    //    //$(".datepicker1").focus(function () {
    //    //    $(".ui-datepicker-month").hide();
    //    //});
    //});

    //$(".datepicker1").datepicker({
    //    dateFormat: 'yy',
    //    changeMonth: false,
    //    changeYear: true,
    //    showButtonPanel: true,

    //    onClose: function (dateText, inst) {
    //       // var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
    //        var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
    //        $(this).val($.datepicker.formatDate('yy', new Date(inst.selectedYear, 1)));
    //        //$(this).val($.datepicker.formatDate('yy', new Date(year, 1)));
    //    }
    //});

    //$(".datepicker1").focus(function () {
    //    $(".ui-datepicker-calendar").hide();
    //    $("#ui-datepicker-div").position({
    //        my: "center top",
    //        at: "center bottom",
    //        of: $(this)
    //    });
    //});

//});
    