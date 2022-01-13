var routeURL = location.protocol + "//" + location.host;
$(document).ready(function () {
    $("#lessonDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false
    });

    InitializeCalendar();
});

function InitializeCalendar() {
    try {


        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                editable: false,
                select: function (event) {
                    onShowModal(event, null);
                }
            });
            calendar.render();
        }

    }
    catch (e) {
        alert(e);
    }

}



function onShowModal(obj, isEventDetail) {
    $("#lessonInput").modal("show");
}

function onCloseModal() {

    $("#lessonInput").modal("hide");
}

function onSubmitForm() {
    if (checkValidation()) {

    
    var requestData = {
        Id: parseInt($("#id").val()),
        Topic: $("#topic").val(),
        Description: $("#description").val(),
        StartDate: $("#lessonDate").val(),
        Duration: $("#duration").val(),
        TeacherId: $("#teacherId").val(),
        StudentId: $("#studentId").val(),
    };

    $.ajax({
        url: routeURL + '/api/Lesson/SaveCalendarData',
        type: 'POST',
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (response) {
            if (response.status === 1 || response.status === 2) {
                $.notify(response.message, "success");
                onCloseModal();
            }
            else {
                $.notify(response.message, "error");
            }
        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
    }
}
function checkValidation() {
    var isValid = true;
    if ($("#topic").val() === undefined || $("#topic").val() === "") {
        isValid = false;
        $("#topic").addClass('error');
    }
    else {
        $("#topic").removeClass('error');
    }

    if ($("#lessonDate").val() === undefined || $("#lessonDate").val() === "") {
        isValid = false;
        $("#lessonDate").addClass('error');
    }
    else {
        $("#lessonDate").removeClass('error');
    }

    return isValid;

}
