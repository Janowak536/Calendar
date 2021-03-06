var routeURL = location.protocol + "//" + location.host;
$(document).ready(function () {
    $("#lessonDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false,
        format: "dd/MM/yyyy HH:mm:ss"
    });

    InitializeCalendar();
});
var calendar;
function InitializeCalendar() {
    try {


        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            calendar = new FullCalendar.Calendar(calendarEl, {
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
                                        backgroundColor: data.isTeacherApproved ? "#28a745" : "#dc3545",
                                        borderColor: "#162466",
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
    if (isEventDetail != null) {

        $("#title").val(obj.title);
        $("#description").val(obj.description);
        $("#lessonDate").val(obj.startDate);
        $("#duration").val(obj.duration);
        $("#teacherId").val(obj.teacherId);
        $("#studentId").val(obj.studentId);
        $("#id").val(obj.id);
        $("#lblStudentName").html(obj.studentName);
        $("#lblTeacherName").html(obj.teacherName);
        if (obj.isTeacherApproved) {
            $("#lblStatus").html('Potwierdzone');
            $("#btnConfirm").addClass("d-none");
            $("#btnSubmit").addClass("d-none");
        } else {
            $("#lblStatus").html('Niepotwierdzone');
            $("#btnConfirm").removeClass("d-none");
            $("#btnSubmit").removeClass("d-none");
        }
        $("#btnDelete").removeClass("d-none");
    }
    else {
        $("#lessonDate").val(obj.startStr + " " + new moment().format("hh:mm A"));
        $("#id").val(0);
        $("#btnDelete").addClass("d-none");
        $("#btnSubmit").removeClass("d-none");
    }
    $("#lessonInput").modal("show");
}

function onCloseModal() {
    $("#lessonForm")[0].reset();
    $("#id").val(0);
    $("#title").val('');
    $("#description").val('');
    $("#lessonDate").val('');
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
                    calendar.refetchEvents();
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

function getEventDetailsByEventId(info) {
    $.ajax({
        url: routeURL + '/api/Lesson/GetCalendarDataById/' + info.id,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {

            if (response.status === 1 && response.dataenum !== undefined) {
                onShowModal(response.dataenum, true);
            }
            successCallback(events);
        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
}

function onTeacherChange() {
    calendar.refetchEvents();
}

function onDeleteLesson() {
    var id = parseInt($("#id").val());
    $.ajax({
        url: routeURL + '/api/Lesson/DeleteLesson/' + id,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {

            if (response.status === 1) {
                $.notify(response.message, "success");
                calendar.refetchEvents();
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

function onConfirm() {
    var id = parseInt($("#id").val());
    $.ajax({
        url: routeURL + '/api/Lesson/ConfirmEvent/' + id,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {

            if (response.status === 1) {
                $.notify(response.message, "success");
                calendar.refetchEvents();
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