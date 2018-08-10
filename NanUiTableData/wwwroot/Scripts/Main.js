layui.use(['layer', 'form'], function () {
    var layer = layui.layer
        , form = layui.form;
    //layer.msg('Hello World');
});

$(function () {
    $("#maxForm").click(() => {
        maxForm();
        $("#maxForm").hide();
        $("#normalForm").show();
    });

    $("#normalForm").click(() => {
        normalForm();
        $("#normalForm").hide();
        $("#maxForm").show();
    });
});

$("#iframe").css("width", $("#contetnLeft").width());
$("#iframe").css("height", $("#contetnLeft").height() * 1 - 75);
$(window).on("resize", function () {
    $("#iframe").css("width", $("#contetnLeft").width());
    $("#iframe").css("height", $("#contetnLeft").height() * 1 - 75);
});

$("#main .tool ul li").click(function () {
    $("#iframe").attr("src", $(this).attr("url"));
});