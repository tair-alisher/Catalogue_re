﻿function pagination() {
    $(document).ready(function () {
        $(document).on("click", "#contentPager a[href]", post_request);
        $("#position-select-list").on("change", post_request_without_page);
        $("#department-select-list").on("change", post_request_without_page);
        $("#administration-select-list").on("change", post_request_without_page);
        $("#division-select-list").on("change", post_request_without_page);
    });
}

function post_request_without_page() {
    var token = $('input[name="__RequestVerificationToken"]').val();
    var name = $("#name").val();
    var positionId = $("#position-select-list").val();
    var departmentId = $("#department-select-list").val();
    var administrationId = $("#administration-select-list").val();
    var divisionId = $("#division-select-list").val();

    $("#loading").show();
    $.ajax({
        url: "/Search/EmployeeFilter",
        type: 'POST',
        data: {
            __RequestVerificationToken: token,
            "name": name,
            "PositionId": positionId,
            "DepartmentId": departmentId,
            "AdministrationId": administrationId,
            "DivisionId": divisionId
        },
        cache: false,
        success: function (result) {
            $("#loading").hide();
            $("#results").html(result);
            $("#accordion").hide();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $("#results").html("Ошибка. Обновите страницу");
            console.log(XMLHttpRequest);
        }
    });
    return false;
}

function post_request() {
    var token = $('input[name="__RequestVerificationToken"]').val();
    var name = $("#name").val();

    var positionId = $("#position-select-list").val();
    var departmentId = $("#department-select-list").val();
    var administrationId = $("#administration-select-list").val();
    var divisionId = $("#division-select-list").val();

    var parts = $(this).attr("href").split("?");
    var url = parts[0];
    var page = parts[1].split("=")[1];

    $("#loading").show();
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            __RequestVerificationToken: token,
            "name": name,
            "page": page,
            "PositionId": positionId,
            "DepartmentId": departmentId,
            "AdministrationId": administrationId,
            "DivisionId": divisionId
        },
        cache: false,
        success: function (result) {
            $("#loading").hide();
            $("#results").html(result);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $("#results").html("Ошибка. Обновите страницу");
            console.log(XMLHttpRequest);
        }
    });
    return false;
}

function check_input(text) {
    $("#ajax-form").submit(function () {
        var input = $("#xyz-search-input").val();
        if (input === "") {
            alert(text);
            return false;
        }
    });
}