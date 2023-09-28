var product = {
    init: function () {
        product.register();
    },
    register: function () {
        $('.btn-images').off('click').on('click', function (e) {
            e.preventDefault();
            $('#imagesManager').modal('show');
            $('#hidProductID').val($(this).data('id'));
            product.loadImages();
        });


        $('#btnChoseImages').off('click').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {

                $('#imageList').append('<div style="margin-left: 5px;display: inline-block;"><img src= "' + url + '" width="60"/><a href="#" class="btnDelImage"><i class="fa-solid fa-xmark"></i></a> </div>');
      
                $('.btnDelImage').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                })
            };

            finder.popup();
        })

        $('#btnSaveImages').off('click').on('click', function (e) {
            var imagesUrl = [];
            var id = $('#hidProductID').val();

            $.each($('#imageList img'), function (i, item) {
                imagesUrl.push($(item).prop('src'));
            })

            $.ajax({
                url: "/Admin/Product/SaveImages",
                type: "POST",
                data: {
                    id: id,
                    images: JSON.stringify(imagesUrl)
                },
                dataType: 'json',
                success: function (res) {
                    if (res.status) {
                        $('#imagesManager').modal('hide');
                        $('#imageList').html('');
                        window.alert("Thêm mới thành công!");
                    }
                }
            })

        })
    },
    loadImages: function () {
        $.ajax({
            url: "/Admin/Product/LoadImages",
            type: "GET",
            data: {
                id: $('#hidProductID').val()
            },
            dataType: 'json',
            success: function (res) {

                var data = res.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<div style="margin-left: 5px;display: inline-block;"><img src= "' + item + '" width="60"/><a href="#" class="btnDelImage"><i class="fa-solid fa-xmark"></i></a></div>'
                });
                $('#imageList').html(html);

                $('.btnDelImage').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                })
            }
        })
    }
}
product.init();