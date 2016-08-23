
var userProfile;

$(document).ready(function () {
    var lock = new Auth0Lock(AUTH0_CLIENT_ID, AUTH0_DOMAIN);
    lock.redirectUrl = AUTH0_CALLBACK_URL;

    var hash = lock.parseHash();
    if (hash) {
        if (hash.error) {
            console.log("There was an error logging in", hash.error);
        }
        else {

            lock.getProfile(hash.id_token, function (err, profile) {
                if (err) {
                    console.log('Cannot get user', err);
                    return;
                }
                userProfile = profile;
                var userid1 = hash.id_token.substring(107);
                var userid = userid1.substring(0,28);

                localStorage.setItem('id_token', hash.id_token);
                localStorage.setItem('email', userProfile.email);
                localStorage.setItem('userid', userid);

                $('.login-box').hide();
                $('.logged-in-box').show();
                $('#ddAccount').show();
                $('.avatar').show();
                $('.name').text('Hello ' + profile.name);
                $('.avatar').attr('src', profile.picture);
                
            });
        }
    }


    $('.btn-login').click(function (e) {
        e.preventDefault();
        lock.show();
    });

    $.ajaxSetup({
        'beforeSend': function (xhr) {
            if (localStorage.getItem('id_token')) {
                xhr.setRequestHeader('Authorization',
                      'Bearer ' + localStorage.getItem('id_token'));
            }
        }
    });

    $('.btn-api').click(function (e) {
        // Just call your API here. The header will be sent
        $.ajax({
            url: 'http://localhost:3001/secured/ping',
            method: 'GET'
        }).then(function (data, textStatus, jqXHR) {
            alert("The request to the secured enpoint was successfull");
        }, function () {
            alert("You need to download the server seed and start it to call this API");
        });
    });

});



    function CheckSession()
    {
        var userid = localStorage.getItem('userid');
        var exp = '';
        
        if (userid == null || userid == undefined)
        {
            console.log('No current session.');
            $('#sessionDate').html('No current session.');
            return;
        }

        var jqxhr = $.get("https://10.100.222.239/api/GetSession/" + userid, function (data) {
            console.log(data);
            exp = data.expires;
        })
        .done(function () {
            //alert("second success");
        })
        .fail(function () {
            alert("error");
        })
        .always(function () {
            //alert("finished");
        });

        $('#sessionDate').html('Session will expire at: ' + exp);
    }