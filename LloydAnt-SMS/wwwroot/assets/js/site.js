// LOGIN POST ACTION
function loginPost() {
    debugger;
    var data = {};
    data.Email = $("#email").val();
    data.Password = $("#password").val();
    let loginViewModel = JSON.stringify(data);
    if (data.Email != "" && data.Password != "") {
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Accounts/Login',
            data:
            {
                data: loginViewModel
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    successAlertWithRedirect(result.msg, result.dashboard)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    } else {
        errorAlert("Password and password confirmation not matched");
    }
}

// Admin Registration POST ACTION
function RegisterUser(){
    debugger;
    var data = {};
    data.FirstName = $('#fname').val();
    data.LastName = $('#lname').val();
    data.Email = $('#email').val();
    data.PhoneNumber = $('#tel').val();
    data.Address = $('#address').val();
    data.Password = $('#password').val();
    data.ConfirmPassword = $('#cpassword').val();
    if (data.FirstName != "" && data.LastName != "" && data.Email != "" && data.PhoneNumber != "" && data.Address != "" && data.Password != "") {
        if (data.Password == data.ConfirmPassword) {
            let applicationViewModel = JSON.stringify(data);
            $.ajax({
                type: 'POST',
                dataType: 'Json',
                url: '/Accounts/CreateUser',
                data:
                {
                    data: applicationViewModel,
                },
                success: function (result) {
                    debugger;
                    if (!result.isError) {
                        successAlertWithRedirect(result.msg, result.url)
                    }
                    else {
                        errorAlert(result.msg)
                    }
                },
                Error: function (ex) {
                    errorAlert(ex);
                }
            });
        }
        else {
            errorAlert("Password and password confirmation not matched");
        }

    }
    else {
        errorAlert("Fill the form completely");
    }

}

function RegisterAdmin() {
    debugger;
    var data = {};
    data.Email = $('#email').val();
    data.Password = $('#password').val();
    data.ConfirmPassword = $('#cpassword').val();
    if (data.Email != "" && data.Password != "") {
        if (data.Password == data.ConfirmPassword) {
            let applicationViewModel = JSON.stringify(data);
            $.ajax({
                type: 'POST',
                dataType: 'Json',
                url: '/Accounts/CreateAdmin',
                data:
                {
                    data: applicationViewModel,
                },
                success: function (result) {
                    debugger;
                    if (!result.isError) {
                        successAlertWithRedirect(result.msg, result.url)
                    }
                    else {
                        errorAlert(result.msg)
                    }
                },
                Error: function (ex) {
                    errorAlert(ex);
                }
            });
        }
        else {
            errorAlert("Password and password confirmation not matched");
        }

    }
    else {
        errorAlert("Fill the form completely");
    }

}


function CreateCourse() {
    debugger;
    var data = {};
    data.Name = $("#cName").val();
    data.Description = $("#desc").val();
    if (data.Description != "" && data.Name != "") {
        let coursedata = data;
        debugger;
        $.ajax({
            type: 'Post',
            dataType: 'json',
            url: '/Admin/CoursePostAction',
            data:
            {
                course: coursedata,
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/Admin/Courses';
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            error: function (ex) {
                errorAlert("Error occured try again");
            }
        });
    }
    else {
        errorAlert("Fill the form completely");
    }
}

function CourseRegistration() {
    debugger;
    var data = {};
    data.CourseIds = $("#courseIds").val();
    let courseData = data;
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Student/RegistrationCourse',
        data:
        {
            course: courseData
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                var url = "/Student/CourseRegistration"
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        Error: function (ex) {
            errorAlert(ex);
        }
    });
}
 
function MappId(Id) {
    debugger
    $("#courseId").val(Id);
}

function AdminCourseDelete() {
    debugger;
    var id = $("#courseId").val();
    if (id != "") {
        let coursId = id;
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Admin/DeletCourse',
            data:
            {
                id: coursId
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = window.location.href;
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    }
    else {
        errorAlert(result.msg)
    }
}

function StudentCourseDelete() {
    debugger;
    var id = $("#courseId").val();
    if (id != "") {
        let coursId = id;
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Student/DeletCourse',
            data:
            {
                id: coursId
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = window.location.href;
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    }
    else {
        errorAlert(result.msg)
    }
}