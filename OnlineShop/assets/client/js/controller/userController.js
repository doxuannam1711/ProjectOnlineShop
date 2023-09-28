var user = {
    init: function () {
        user.loadProvince();
        user.regEvent();
        user.regEventPrecinct();
    },
    regEvent: function () {
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                user.loadDistrict(parseInt(id));
            } else {
                $('#ddlDistrict').html('');
            }
        })
    },
    regEventPrecinct: function () {
        $('#ddlDistrict').off('change').on('change', function () {
            var idDistrict = $('#ddlDistrict').val();
            var idProvince = $('#ddlProvince').val();

            if (idDistrict != '' && idProvince != '') {
                user.loadPrecinct(parseInt(idProvince), parseInt(idDistrict));
            } else {
                $('#ddlPrecinct').html('');
            }
        })
    },
    loadProvince: function () {

        $.ajax({
            url: "/User/LoadProvince",
            type: "POST",
            dataType: "json",
            success: function (res) {
                var html = '<option value="">--Chọn tỉnh thành--</option>';
                if (res.status == true) {
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlProvince').html(html);
                }
            }
        })
    },

    loadDistrict: function (id) {

        $.ajax({
            url: "/User/LoadDistrict",
            type: "POST",
            data: { provinceID: id },
            dataType: "json",
            success: function (res) {
                var html = '<option value="">--Chọn quận huyện--</option>';
                if (res.status == true) {
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlDistrict').html(html);
                }
            }
        })
    },

    loadPrecinct: function (provinceID, districtID) {

        $.ajax({
            url: "/User/LoadPrecinct",
            type: "POST",
            data: { provinceID: provinceID, districtID: districtID },
            dataType: "json",
            success: function (res) {
                var html = '<option value="">--Chọn khu vực--</option>';
                if (res.status == true) {
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlPrecinct').html(html);
                }
            }
        })
    },
}
user.init();