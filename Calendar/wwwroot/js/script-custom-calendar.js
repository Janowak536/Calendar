var routeURL = location.protocol + "//" + location.host;
$(document).ready(function () {
    $("#lessonDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false,
        format: "dd/MM/yyyy HH:mm:ss" // I have researched this and added this in myself which works for my region.
    });
    InitializeCalendar();
});
var calendar;
function InitializeCalendar() {
    try {


        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            calendar = new FullCalendar.Calendar(calendarEl,
                {
                    initialView: 'dayGridMonth',
                    headerToolbar: {
                        left: 'prev,next,today',
                        center: 'title',
                        right: 'dayGridMonth,timeGridWeek,timeGridDay'
                    },
                    timeZone: 'Europe/Berlin', // find your country time zone and replace the part in '' here.
                    selectable: true,
                    editable: false,
                    select: function (event) {
                        onShowModal(event, null);
                    },
                    eventDisplay: 'block',
                    events: function (fetchInfo, successCallback, failureCallback) {
                        $.ajax({
                            url: routeURL + '/api/Lesson/GetCalendarData?teacherId=' + $("#teacherId").val(),
                            type: 'GET',
                            dataType: 'JSON',
                            success: function (response) {
                                var events = [];
                                if (response.status === 1) {
                                    $.each(response.dataenum, function (i, data) {
                                        events.push({
                                            title: data.title,

                                            description: data.description,
                                            start: data.startDate,
                                            end: data.endDate,
                                            backgroundColor: data.isTeacherApproved ? "#30c953" : "#c21f2f",
                                            borderColor: "#142e9c",
                                            textColor: "white",
                                            id: data.id

                                        });
                                    })
                                }
                                successCallback(events);
                            },
                            error: function (xhr) {
                                $.notify("Error", "error");
                            }
                        });
                    },
                    eventClick: function (info) {
                        getEventDetailsByEventId(info.event);
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
        Title: $("#title").val(),
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
    if ($("#title").val() === undefined || $("#title").val() === "") {
        isValid = false;
        $("#title").addClass('error');
    }
    else {
        $("#title").removeClass('error');
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
