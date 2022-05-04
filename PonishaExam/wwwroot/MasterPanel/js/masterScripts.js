$(document).ready(function () {
    $("#lessonExam").change(function () {
        var lessonId = $("#lessonExam").val();

        $.getJSON("/GetGrade/" + lessonId,
            function (sub) {
                var id = sub.id;
                var title = sub.title;
                $("#gradeId").val(id);
                $("#grade").val(title);
            });
    });
});